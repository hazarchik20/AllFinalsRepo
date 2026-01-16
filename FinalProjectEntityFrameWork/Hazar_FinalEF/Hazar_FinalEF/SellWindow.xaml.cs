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
using BLL.Models;

namespace Hazar_FinalEF
{
    /// <summary>
    /// Interaction logic for SellWindow.xaml
    /// </summary>
    public partial class SellWindow : Window
    {
        public People people { get; set; }
        public SellWindow()
        {
            InitializeComponent();
        }

        private void SELLBt_Click(object sender, RoutedEventArgs e)
        {
            if (MusicIsValid())
            {
                people = new People()
                {
                    Name = this.NameTB.Text,
                    LastName = this.LastNameTB.Text,
                    Phone = this.PhoneTB.Text,
                    Birthday = Convert.ToDateTime(this.BirthdayTB.SelectedDate),
                    
                };
                this.Close();
            }
        }
        private void ClearBorders()
        {
            this.NameTB.BorderBrush = Brushes.LightCyan;
            this.LastNameTB.BorderBrush = Brushes.LightCyan;
            this.BirthdayTB.BorderBrush = Brushes.LightCyan;
            this.PhoneTB.BorderBrush = Brushes.LightCyan;
        }

        private bool MusicIsValid()
        {
            ClearBorders();
            if (String.IsNullOrEmpty(this.NameTB.Text) || String.IsNullOrWhiteSpace(this.NameTB.Text))
            {
                this.NameTB.BorderBrush = Brushes.Red;
                return false;
            }
            if (String.IsNullOrEmpty(this.LastNameTB.Text) || String.IsNullOrWhiteSpace(this.LastNameTB.Text))
            {
                this.LastNameTB.BorderBrush = Brushes.Red;
                return false;
            }
            if (String.IsNullOrEmpty(this.BirthdayTB.Text) || String.IsNullOrWhiteSpace(this.BirthdayTB.Text))
            {
                this.BirthdayTB.BorderBrush = Brushes.Red;
                return false;
            }
            if (String.IsNullOrEmpty(this.PhoneTB.Text) || String.IsNullOrWhiteSpace(this.PhoneTB.Text))
            {
                this.PhoneTB.BorderBrush = Brushes.Red;
                return false;
            }
            return true;
        }
    }
}
