using System;
using System.Data;
using System.Windows;

namespace CardIndex.DataBase
{
    public interface IWindow
    {
        event EventHandler Closed;
        void Show();
        Window Owner { set; }
    }
    class CardTable:ITable
    {
        private DataTable table = new DataTable("Cards");

        public CardTable()
        {
            CreateColumn(typeof(int), "Id");
            CreateColumn(typeof(string), "ФИО");
            CreateColumn(typeof(string), "Пол");
            CreateColumn(typeof(string), "Дата рождения");
            CreateColumn(typeof(string), "Адрес");
            CreateColumn(typeof(string), "Телефон");
            UpdateTable();
        }

        public DataView DataView => table.DefaultView;
        public void Create(Window owner)
        {
            OpenCardWindow(new CardWindow(),owner);
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            UpdateTable();
        }
        public void Edit(object primaryKey, Window owner)
        {
            OpenCardWindow(new CardWindow(primaryKey),owner);
        }

        public void UpdateTable()
        {
            using (CardContext db = new CardContext())
            {
                table.Clear();
                foreach (Card card in db.Cards)
                {
                    DataRow row = table.NewRow();
                    row["Id"] = card.Id;
                    row["ФИО"] = card.FIO;
                    row["Пол"] = card.Gender;
                    row["Дата рождения"] = card.Birthday;
                    row["Адрес"] = card.Address;
                    row["Телефон"] = card.Phone;
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
        private void OpenCardWindow(IWindow cardWindow,Window owner)
        {
            cardWindow.Closed += Window_Closed;
            cardWindow.Owner = owner;
            cardWindow.Show();
        }
        public void Delete(object primaryKey)
        {
            using(CardContext db = new CardContext())
            {
                db.Cards.Remove(db.Cards.Find(primaryKey));
                db.SaveChanges();
                UpdateTable();
            }
        }
    }
}
