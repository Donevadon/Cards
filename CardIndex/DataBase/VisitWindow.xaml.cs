using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CardIndex.DataBase
{
    /// <summary>
    /// Логика взаимодействия для VisitWindow.xaml
    /// </summary>
    public partial class VisitWindow : Window , IWindow
    {
        private Visit visit = new Visit();
        private Card card;
        public VisitWindow(Card card)
        {
            this.card = card;
            InitializeComponent();
        }

        public VisitWindow(Card card,object primaryKey)
        {
            InitializeComponent();
            this.card = card;
            InitialVisit(primaryKey);
        }

        private void InitialVisit(object primaryKey)
        {
            using (CardContext db = new CardContext())
            {
                visit = db.Visits.Find(primaryKey);
            }
            OutputData(visit);
        }


        private void OutputData(Visit visit)
        {
            dateVisit.SelectedDate = DateTime.Parse(visit.DateVitis);
            if (visit.TypeVisit == 0)
            {
                primary.IsChecked = true;
                secondary.IsChecked = false;
            }
            else
            {
                primary.IsChecked = false;
                secondary.IsChecked = true;
            }
            diagnosis.Text = visit.Diagnosis;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void Save()
        {
            using (CardContext db = new CardContext())
            {
                visit = db.Visits.Find(visit.Id) is null ? db.Visits.Add(visit) : db.Visits.Find(visit.Id);
                visit.DateVitis = dateVisit.SelectedDate.Value.ToString("D");
                visit.IdCard = card.Id;
                visit.TypeVisit = primary.IsChecked.Value ? 0 : 1;
                visit.Diagnosis = diagnosis.Text;
                db.SaveChanges();
                Close();
            }
        }
    }
}
