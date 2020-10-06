using CardIndex.DataBase;
using System;
using System.Data;
using System.Windows;

namespace CardIndex
{
    /// <summary>
    /// Логика взаимодействия для CardWindow.xaml
    /// </summary>
    public partial class CardWindow : Window,IWindow
    {
        private Card card = new Card();
        private IVisitTable<Card> table;

        public CardWindow(object primaryKey)
        {
            InitializeComponent();
            InitialCard(primaryKey);
            InitialTable();
        }

        public CardWindow()
        {
            InitializeComponent();
            InitialTable();
        }
        /// <summary>
        /// Установить выбранную карту в окно
        /// </summary>
        /// <param name="primaryKey"></param>
        private void InitialCard(object primaryKey)
        {
            using (CardContext db = new CardContext())
            {
                card = db.Cards.Find(primaryKey);
            }
            OutputData(card);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveCard();
        }
        /// <summary>
        /// Вывод данных карты в окно
        /// </summary>
        /// <param name="card"></param>
        private void OutputData(Card card)
        {
            fio.Text = card.FIO;
            if (card.Gender == "М") 
            {
                genderM.IsChecked = true;
                genderW.IsChecked = false;
            }
            else
            {
                genderM.IsChecked = false;
                genderW.IsChecked = true;
            }
            date.SelectedDate = DateTime.Parse(card.Birthday);
            address.Text = card.Address;
            phone.Text = card.Phone;
        }
        /// <summary>
        /// Сохранить данные карты в базе данных
        /// </summary>
        private void SaveCard()
        {
            using (CardContext db = new CardContext())
            {
                try
                {
                    card = db.Cards.Find(card.Id) is null ? db.Cards.Add(card) : db.Cards.Find(card.Id);
                    card.FIO = fio.Text;
                    card.Gender = genderM.IsChecked.Value ? genderM.Content.ToString() : genderW.Content.ToString();
                    card.Birthday = date.SelectedDate.Value.ToString("D");
                    card.Address = address.Text;
                    card.Phone = phone.Text;
                    db.SaveChanges();
                    Close();
                }
                catch 
                {

                }
            }
        }

        private void InitialTable()
        {
            table = new VisitTable(card);
            dbVisits.ItemsSource = table.DataView;
            dbVisits.MouseDoubleClick += Edit_Clicked;
        }

        private void Edit_Clicked(object sender, RoutedEventArgs e)
        {
            DataRowView rowView = dbVisits.SelectedItem as DataRowView;
            table.Edit(rowView.Row.ItemArray[0], this);
        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowView = dbVisits.SelectedItem as DataRowView;
            table.Delete(rowView.Row.ItemArray[0]);
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            table.UpdateTable();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            table.Create(this);
        }


    }
}
