using System;
using System.Windows;
using CardIndex.DataBase;
using System.Data;

namespace CardIndex
{
    public interface IVisitTable<out T> : ITable where T : Card
    {
        T Card { get; }
    }
    public interface ITable
    {
        DataView DataView { get; }
        void Create(Window owner);
        void UpdateTable();
        void Edit(object primaryKey, Window owner);
        void Delete(object primaryKey);
    }
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ITable dataTable;
        public MainWindow()
        {
            InitializeComponent();
            InitialTable();
        }

        private void CreateCard_Click(object sender, RoutedEventArgs e)
        {
            dataTable.Create(this);
        }

        private void InitialTable()
        {
            dataTable = new CardTable();
            cardTable.ItemsSource = dataTable.DataView;
            cardTable.MouseDoubleClick += EditCard_Clicked;
        }


        private void EditCard_Clicked(object sender, RoutedEventArgs e)
        {
            DataRowView rowView = cardTable.SelectedItem as DataRowView;
            dataTable.Edit(rowView.Row.ItemArray[0],this);
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            dataTable.UpdateTable();
        }

        private void DeleteCard_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowView = cardTable.SelectedItem as DataRowView;
            dataTable.Delete(rowView.Row.ItemArray[0]);
        }
    }
}
