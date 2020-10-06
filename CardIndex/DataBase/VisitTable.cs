using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CardIndex.DataBase
{
    class VisitTable : IVisitTable<Card>
    {
        private DataTable table = new DataTable("Visits");
        public Card Card { get; set; }

        public VisitTable(Card card)
        {
            Card = card;
            CreateColumn(typeof(int), "Id");
            CreateColumn(typeof(string), "Дата");
            CreateColumn(typeof(string), "Тип");
            CreateColumn(typeof(string), "Диагноз");
            UpdateTable();
        }

        public DataView DataView => table.DefaultView;


        public void Create(Window owner)
        {
            OpenVisitWindow(new VisitWindow(Card),owner);
        }

        public void Delete(object primaryKey)
        {
            using (CardContext db = new CardContext())
            {
                db.Visits.Remove(db.Visits.Find(primaryKey));
                db.SaveChanges();
                UpdateTable();
            }

        }

        public void Edit(object primaryKey, Window owner)
        {
            OpenVisitWindow(new VisitWindow(Card,primaryKey), owner);
        }

        public void UpdateTable()
        {
            using (CardContext db = new CardContext())
            {
                table.Clear();
                var visits = from visit in db.Visits
                             where visit.IdCard == Card.Id
                             select visit;

                foreach (Visit visit in visits)
                {
                    DataRow row = table.NewRow();
                    row["Id"] = visit.Id;
                    row["Дата"] = visit.DateVitis;
                    row["Тип"] = visit.TypeVisit == 0 ? "Первичный" : "Вторичный";
                    row["Диагноз"] = visit.Diagnosis;
                    table.Rows.Add(row);
                }
            }

        }

        /// <summary>
        /// Создать колонку таблицы
        /// </summary>
        /// <param name="type"></param>
        /// <param name="Name"></param>
        private void CreateColumn(Type type, string Name)
        {
            DataColumn column = new DataColumn();
            column.DataType = type;
            column.ColumnName = Name;
            column.ReadOnly = true;
            table.Columns.Add(column);
        }

        /// <summary>
        /// Открыть окно сарточки
        /// </summary>
        /// <param name="cardWindow"></param>
        /// <param name="owner"></param>
        private void OpenVisitWindow(IWindow cardWindow, Window owner)
        {
            cardWindow.Closed += Window_Closed;
            cardWindow.Owner = owner;
            cardWindow.Show();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            UpdateTable();
        }

    }
}
