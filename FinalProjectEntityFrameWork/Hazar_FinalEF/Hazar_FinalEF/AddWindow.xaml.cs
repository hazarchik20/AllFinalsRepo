using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Reflection;
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
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public MusicRecord musicRecord { get; set; }
        public AddWindow()
        {
            InitializeComponent();
        }
        Genre_MusicRecord myGenre;
        private void ADDBt_Click(object sender, RoutedEventArgs e)
        {
            if (MusicIsValid())
            {
                List<REALmusicRecord> Templist = new List<REALmusicRecord>();
                for(int i = 0; i<Convert.ToInt32(RealCountTB.Text); i++)
                {
                    REALmusicRecord temp = new REALmusicRecord();
                    Templist.Add(temp);
                }
                musicRecord = new MusicRecord()
                {
                    NameGroup = this.NameGroupTB.Text,
                    NamePublisher = this.NamePublisherTB.Text,
                    NameRecord = this.NameRecordTB.Text,
                    Birthday = Convert.ToDateTime(this.birthdayTb.SelectedDate),
                    Cost = Convert.ToInt32(this.CostTB.Text),
                    Cost_for_sale = Convert.ToInt32(this.Cost_for_saleTB.Text),
                    CountSong = Convert.ToInt32(this.CountSongTB.Text),
                    genre_MusicRecord = myGenre,
                    RealmusicRecord = Templist,
                };
                this.Close();
            }
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            if(((sender as ComboBoxItem).Content as StackPanel)!=null)
            switch ((((sender as ComboBoxItem).Content as StackPanel).Children[0] as TextBlock).Text)
            {
                case "Classical_music": { myGenre = Genre_MusicRecord.Classical_music; } break;
                case "Country": { myGenre = Genre_MusicRecord.Country; } break;
                case "Electronic_dance_music": { myGenre = Genre_MusicRecord.Electronic_dance_music; } break;
                case "Hip_hop": { myGenre = Genre_MusicRecord.Hip_hop; } break;
                case "Jazz": { myGenre = Genre_MusicRecord.Jazz; } break;
                case "Rock": { myGenre = Genre_MusicRecord.Rock; } break;
                case "K_pop": { myGenre = Genre_MusicRecord.K_pop; } break;
                case "Latin_music": { myGenre = Genre_MusicRecord.Latin_music; } break;
                case "Pop": { myGenre = Genre_MusicRecord.Pop; } break;
            }
        }
        private void ComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
           
        }

        public void SetOptions(string title, string buttonText)
        {
            this.titleTb.Text = title;
            this.editBt.Content = buttonText;
        }

        public void SetMusicRecord(MusicRecord music)
        {
            this.NameGroupTB.Text = music.NameGroup;
            this.NamePublisherTB.Text = music.NamePublisher;
            this.NameRecordTB.Text = music.NameRecord;
            this.birthdayTb.SelectedDate = music.Birthday;
            this.Cost_for_saleTB.Text = music.Cost_for_sale.ToString();
            this.CostTB.Text = music.Cost.ToString();
            this.CountSongTB.Text = music.CountSong.ToString();
            this.RealCountTB.Text = music.RealmusicRecord.Count.ToString();
           
        }
       
        private void ClearBorders()
        {
            this.NameGroupTB.BorderBrush = Brushes.LightCyan;
            this.NamePublisherTB.BorderBrush = Brushes.LightCyan;
            this.NameRecordTB.BorderBrush = Brushes.LightCyan;
            this.birthdayTb.BorderBrush = Brushes.LightCyan;
            this.CountSongTB.BorderBrush = Brushes.LightCyan;
            this.CostTB.BorderBrush = Brushes.LightCyan;
            this.Cost_for_saleTB.BorderBrush = Brushes.LightCyan;
            this.genre_MusicRecordCB.BorderBrush = Brushes.LightCyan;
            this.RealCountTB.BorderBrush = Brushes.LightCyan;
        }

        private bool MusicIsValid()
        {
            ClearBorders();
            if (String.IsNullOrEmpty(this.NameGroupTB.Text) || String.IsNullOrWhiteSpace(this.NameGroupTB.Text))
            {
                this.NameGroupTB.BorderBrush = Brushes.Red;
                return false;
            }
            if (String.IsNullOrEmpty(this.NamePublisherTB.Text) || String.IsNullOrWhiteSpace(this.NamePublisherTB.Text))
            {
                this.NamePublisherTB.BorderBrush = Brushes.Red;
                return false;
            }
            if (String.IsNullOrEmpty(this.birthdayTb.Text) || String.IsNullOrWhiteSpace(this.birthdayTb.Text))
            {
                this.birthdayTb.BorderBrush = Brushes.Red;
                return false;
            }
            if (String.IsNullOrEmpty(this.NameRecordTB.Text) || String.IsNullOrWhiteSpace(this.NameRecordTB.Text))
            {
                this.NameRecordTB.BorderBrush = Brushes.Red;
                return false;
            }
            int temp;
            if (!Int32.TryParse(this.CountSongTB.Text, out temp) || temp <= 0)
            {
                this.CountSongTB.BorderBrush = Brushes.Red;
                return false;
            }
            
            if (!Int32.TryParse(this.CostTB.Text, out temp) || temp <= 0)
            {
                this.CostTB.BorderBrush = Brushes.Red;
                return false;
            }
           
            if (!Int32.TryParse(this.Cost_for_saleTB.Text, out temp) || temp <= 0)
            {
                this.Cost_for_saleTB.BorderBrush = Brushes.Red;
                return false;
            }

            if (!Int32.TryParse(this.RealCountTB.Text, out temp) || temp <= 0)
            {
                this.RealCountTB.BorderBrush = Brushes.Red;
                return false;
            }
            if (String.IsNullOrEmpty(this.genre_MusicRecordCB.Text) || String.IsNullOrWhiteSpace(this.genre_MusicRecordCB.Text))
            {
                this.genre_MusicRecordCB.BorderBrush = Brushes.Red;
                return false;
            }
            return true;
        }

        
    }
}
