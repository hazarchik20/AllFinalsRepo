using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using BLL.Interfaces;
using BLL.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Hazar_FinalEF
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {



        //_________________________________________________ Zminni _________________________________________________\\
        #region Zminni
        private IServices<MusicRecord> _service;
        public string userSearchingChoise;
        public string userSearchingComboBoxChoise;
        public DateTime userPopularChoiseDate;
        public bool ByWEEK = false;
        public bool ByYEAR = false;
        public bool ByMONTH = false;
        public string userPopularChoise;
        public string userPopularComboBoxChoise;
        public ObservableCollection<MusicRecord> MusicRecords { get; set; }
        public AddWindow Add { get; set; } 
        public SellWindow Sell { get; set; }
        #endregion
        //________________________________________________ ! Zminni ! _________________________________________________\\



        public AdminWindow(IServices<MusicRecord> serviceODJ)
        {
            InitializeComponent();
            _service = serviceODJ;
            MusicRecords = new ObservableCollection<MusicRecord>();
            InitializedCollection();
        }



        //_________________________________________________ Fundament Metod _________________________________________________\\
        #region Fundament Metod
        private void InitializedCollection()
        {
            MusicRecords.Clear();
            foreach (var music in this._service.GetAll())
            {
                MusicRecords.Add(music);
            }
            this.ShowAllGrid.ItemsSource = MusicRecords;
        }
        private void addMusicRercordBT_Click(object sender, RoutedEventArgs e)
        {
            Add = new AddWindow();
            Add.SetOptions("ADD MUSIC RECORD", "ADD");
            Add.ShowDialog();
            if (Add.musicRecord != null)
            {
                this._service.Add(Add.musicRecord);
                InitializedCollection();
            }
        }
        
        private void RadioButtonShow_Checked(object sender, RoutedEventArgs e)
        {
        }
        private void showMusicRercordBT_Click(object sender, RoutedEventArgs e)
        {
            InitializedCollection();
        }
        private void deleteMusicRercordBT_Click(object sender, RoutedEventArgs e)
        {
            if (this.ShowAllGrid.SelectedItem != null && this.ShowAllGrid.SelectedItem is MusicRecord)
            {
                MusicRecord tempPeople = this.ShowAllGrid.SelectedItem as MusicRecord;
                this._service.Delete(tempPeople);
                InitializedCollection();
            }
        }
        private void updateMusicRercordBT_Click(object sender, RoutedEventArgs e)
        {
            if (this.ShowAllGrid.SelectedItem != null && this.ShowAllGrid.SelectedItem is MusicRecord)
            {
                int id = (this.ShowAllGrid.SelectedItem as MusicRecord).Id;
                Add = new AddWindow();
                Add.SetOptions("UPDATE MUSIC RECORD", "Edit");
                Add.SetMusicRecord(this.ShowAllGrid.SelectedItem as MusicRecord);
                Add.ShowDialog();
                if (Add.musicRecord != null)
                {
                    Add.musicRecord.Id = id;
                    this._service.Update(Add.musicRecord);
                    InitializedCollection();
                }
            }
        }
        private void WriteOffBT_Click(object sender, RoutedEventArgs e)
        {
            if(WriteOffTB.Text != null && WriteOffTB.Text != "")
            {
                if (this.ShowAllGrid.SelectedItem != null && this.ShowAllGrid.SelectedItem is MusicRecord)
                {
                    MusicRecord tempMusic = (this.ShowAllGrid.SelectedItem as MusicRecord);
                    for(int i = 0; i < Convert.ToInt32(WriteOffTB.Text); i++)
                    {
                        tempMusic.RealmusicRecord.Remove(tempMusic.RealmusicRecord[(tempMusic.RealmusicRecord.Count) - 1]);
                    }
                    this._service.Update(tempMusic);
                    InitializedCollection();
                }
            }

        }
        #endregion
        //_______________________________________________ ! Fundament Metod ! _______________________________________________\\




        //_________________________________________________ SEARCHING _________________________________________________\\
        #region SEARCHING
        private void RadioButtonSearching_Checked(object sender, RoutedEventArgs e)
        {
            userSearchingChoise = Convert.ToString((sender as RadioButton).Content);
        }
        private void ComboBoxItemSearching_Selected(object sender, RoutedEventArgs e)
        {
            userSearchingComboBoxChoise = ((((sender as ComboBoxItem).Content as StackPanel).Children[0]) as TextBlock).Text;
        }
        private void SearchingMusicRercordBT_Click(object sender, RoutedEventArgs e)
        {
            if (userSearchingChoise != null)
            {
                switch (userSearchingChoise)
                {
                    case "Group Name":
                        {
                            if (!((String.IsNullOrEmpty(this.SearchingTB.Text) || String.IsNullOrWhiteSpace(this.SearchingTB.Text))))
                            {
                                MusicRecords.Clear();
                                foreach (var music in this._service.GetAll())
                                {
                                    if (music.NameGroup == this.SearchingTB.Text)
                                        MusicRecords.Add(music);
                                }
                                this.ShowAllGrid.ItemsSource = MusicRecords;
                            }
                            else
                            {
                                MessageBox.Show("Please, enter group name ", " ", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        break;
                    case "Name record":
                        {
                            if (!((String.IsNullOrEmpty(this.SearchingTB.Text) || String.IsNullOrWhiteSpace(this.SearchingTB.Text))))
                            {
                                MusicRecords.Clear();
                                foreach (var music in this._service.GetAll())
                                {
                                    if (music.NameRecord == this.SearchingTB.Text)
                                        MusicRecords.Add(music);
                                }
                                this.ShowAllGrid.ItemsSource = MusicRecords;
                            }
                            else
                            {
                                MessageBox.Show("Please, enter name record", " ", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        break;
                    case "Genre":
                        {
                            if (userSearchingComboBoxChoise != null)
                            {
                                MusicRecords.Clear();
                                foreach (var music in this._service.GetAll())
                                {
                                    if (Convert.ToString(music.genre_MusicRecord) == userSearchingComboBoxChoise)
                                        MusicRecords.Add(music);
                                }
                                this.ShowAllGrid.ItemsSource = MusicRecords;
                            }
                            else
                            {
                                MessageBox.Show("Please,enter genre  ", " ", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        break;
                }
            }
            else
            {
                MessageBox.Show("Please, choose what to search for", " ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ComboBoxSearching_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        #endregion
        //________________________________________________ ! SEARCHING ! _________________________________________________\\




        //_________________________________________________ work with PEOPLE _________________________________________________\\
        #region work with PEOPLE
        private void SellMusicRercordBT_Click(object sender, RoutedEventArgs e)
        {
            if (this.ShowAllGrid.SelectedItem != null && this.ShowAllGrid.SelectedItem is MusicRecord)
            {
                MusicRecord tempMusic = (this.ShowAllGrid.SelectedItem as MusicRecord);
                Sell = new SellWindow();
                Sell.ShowDialog();
                if (Sell.people != null)
                {
                    SOLDmusicRecord tempSold = new SOLDmusicRecord
                    {
                        People = new People
                        {
                            Name = Sell.people.Name,
                            LastName = Sell.people.LastName,
                            Birthday = Sell.people.Birthday,
                            Phone = Sell.people.Phone,
                        },
                        DateSold = DateTime.Now
                    };
                    tempMusic.SoldmusicRecord.Add(tempSold);
                    tempMusic.RealmusicRecord.Remove(tempMusic.RealmusicRecord[(tempMusic.RealmusicRecord.Count) - 1]);
                    this._service.Update(tempMusic);
                    InitializedCollection();
                }
            }
        }
        private void PostPoneBT_Click(object sender, RoutedEventArgs e)
        {
            if (this.ShowAllGrid.SelectedItem != null && this.ShowAllGrid.SelectedItem is MusicRecord)
            {
                MusicRecord tempMusic = (this.ShowAllGrid.SelectedItem as MusicRecord);
                Sell = new SellWindow();
                Sell.ShowDialog();
                if (Sell.people != null)
                {
                    VIDKLADmusicRecord tempWidklad = new VIDKLADmusicRecord
                    {
                        People = new People
                        {
                            Name = Sell.people.Name,
                            LastName = Sell.people.LastName,
                            Birthday = Sell.people.Birthday,
                            Phone = Sell.people.Phone,
                        },
                        DateVidklad = DateTime.Now
                    };
                    tempMusic.VidkladmusicRecord.Add(tempWidklad);
                    tempMusic.RealmusicRecord.Remove(tempMusic.RealmusicRecord[(tempMusic.RealmusicRecord.Count) - 1]);
                    this._service.Update(tempMusic);
                    InitializedCollection();
                }
            }
        }
        private void ActinMusicRercordBT_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
        //________________________________________________ ! work with PEOPLE ! _________________________________________________\\




        //_________________________________________________ MOST POPULAR _________________________________________________\\
        #region MOST POPULAR
        private void RadioButtonPopular_Checked(object sender, RoutedEventArgs e)
        {
            userPopularChoise = Convert.ToString((sender as RadioButton).Content);
        }
        private void RadioButtonPopularTime_Checked(object sender, RoutedEventArgs e)
        {
            string tempstr = Convert.ToString((sender as RadioButton).Content);
            DateTime tempDate = DateTime.Now;
            switch (tempstr)
            {
                case "Week":
                    {
                        if (tempDate.Day - 7 > 0)
                            userPopularChoiseDate = new DateTime(tempDate.Year, tempDate.Month, tempDate.Day - 7);
                        else
                            userPopularChoiseDate = new DateTime(tempDate.Year, tempDate.Month - 1, tempDate.Day - 7 + 30);
                        ByYEAR = false;
                        ByMONTH = false;
                        ByWEEK = true;

                    }
                    break;

                case "Month":
                    {
                        if (tempDate.Month - 1 > 0)
                            userPopularChoiseDate = new DateTime(tempDate.Year, tempDate.Month - 1, tempDate.Day);
                        else
                            userPopularChoiseDate = new DateTime(tempDate.Year - 1, tempDate.Month - 1 + 12, tempDate.Day);
                        ByYEAR = false;
                        ByMONTH = true;
                        ByWEEK = false;
                    }
                    break;
                case "Year": 
                    {
                        userPopularChoiseDate = new DateTime(tempDate.Year-1, tempDate.Month , tempDate.Day);
                        ByYEAR = true;
                        ByMONTH = false;
                        ByWEEK = false;
                    } 
                    break;
            }
        }
        private void ComboBoxItemPopular_Selected(object sender, RoutedEventArgs e)
        {
            userPopularComboBoxChoise = ((((sender as ComboBoxItem).Content as StackPanel).Children[0]) as TextBlock).Text;
        }
        private void ComboBoxPopular_SelectionChanged(object sender, RoutedEventArgs e)
        {
            
        }
        public int CountSOLDRecordBytime(MusicRecord tempMusic)
        {
            int number = 0;
            if (ByWEEK)
            {
                for (int i = 0; i < tempMusic.SoldmusicRecord.Count; i++)
                {
                    if (tempMusic.SoldmusicRecord[i].DateSold.Year >= userPopularChoiseDate.Year)
                    {
                        if (tempMusic.SoldmusicRecord[i].DateSold.Month >= userPopularChoiseDate.Month)
                        {
                            if (tempMusic.SoldmusicRecord[i].DateSold.Day >= userPopularChoiseDate.Day && tempMusic.SoldmusicRecord[i].DateSold.Day <= DateTime.Now.Day)
                            {
                                number++;
                            }
                        }
                    }
                }
            }
            else if (ByMONTH)
            {
                for (int i = 0; i < tempMusic.SoldmusicRecord.Count; i++)
                {
                    if (tempMusic.SoldmusicRecord[i].DateSold.Year >= userPopularChoiseDate.Year)
                    {
                        if (tempMusic.SoldmusicRecord[i].DateSold.Month >= userPopularChoiseDate.Month && tempMusic.SoldmusicRecord[i].DateSold.Month <= DateTime.Now.Month)
                        {

                            number++;

                        }
                    }
                }
            }
            else if (ByMONTH)
            {
                for (int i = 0; i < tempMusic.SoldmusicRecord.Count; i++)
                {
                    if (tempMusic.SoldmusicRecord[i].DateSold.Year >= userPopularChoiseDate.Year && tempMusic.SoldmusicRecord[i].DateSold.Year <= DateTime.Now.Year)
                    {
                        number++;
                    }
                }
            }
            return number;
        }
        private void MostPopularBT_Click(object sender, RoutedEventArgs e)
        {
            if (userPopularChoise != null)
            {
                List<MusicRecord> TempListBySort = new List<MusicRecord>();

                switch (userPopularChoise)
                {
                    case "ALL Record":
                        {
                            MusicRecords.Clear();
                            foreach (var music in this._service.GetAll())
                            {
                                TempListBySort.Add(music);
                            }
                            for (var i = 1; i < TempListBySort.Count; i++)
                            {
                                for (var j = 0; j < TempListBySort.Count - i; j++)
                                {
                                    if (CountSOLDRecordBytime(TempListBySort[j]) > CountSOLDRecordBytime(TempListBySort[j+1]))
                                    {
                                        var temp = TempListBySort[j];
                                        TempListBySort[j] = TempListBySort[j + 1];
                                        TempListBySort[j + 1] = temp;
                                    }
                                }
                            }
                            foreach (var music in TempListBySort)
                            {
                                MusicRecords.Add(music);
                            }
                            this.ShowAllGrid.ItemsSource = MusicRecords;

                        }
                        break;
                    case "AVTOR":
                        {
                            if (!((String.IsNullOrEmpty(this.MostPopularTB.Text) || String.IsNullOrWhiteSpace(this.MostPopularTB.Text))))
                            {
                                MusicRecords.Clear();
                                foreach (var music in this._service.GetAll())
                                {
                                    if (music.NameRecord == this.MostPopularTB.Text)
                                        TempListBySort.Add(music);
                                }
                                for (var i = 1; i < TempListBySort.Count; i++)
                                {
                                    for (var j = 0; j < TempListBySort.Count - i; j++)
                                    {
                                        if (CountSOLDRecordBytime(TempListBySort[j]) > CountSOLDRecordBytime(TempListBySort[j + 1]))
                                        {
                                            var temp = TempListBySort[j];
                                            TempListBySort[j] = TempListBySort[j + 1];
                                            TempListBySort[j + 1] = temp;
                                        }
                                    }
                                }
                                foreach (var music in TempListBySort)
                                {
                                    MusicRecords.Add(music);
                                }
                                this.ShowAllGrid.ItemsSource = MusicRecords;
                            }
                            else
                            {
                                MessageBox.Show("Please, enter name record", " ", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        break;
                    case "GENRE":
                        {
                            if (userPopularComboBoxChoise != null)
                            {
                                MusicRecords.Clear();
                                foreach (var music in this._service.GetAll())
                                {
                                    if (Convert.ToString(music.genre_MusicRecord) == userPopularComboBoxChoise)
                                        TempListBySort.Add(music);
                                }
                                for (var i = 1; i < TempListBySort.Count; i++)
                                {
                                    for (var j = 0; j < TempListBySort.Count - i; j++)
                                    {
                                        if (CountSOLDRecordBytime(TempListBySort[j]) > CountSOLDRecordBytime(TempListBySort[j + 1]))
                                        {
                                            var temp = TempListBySort[j];
                                            TempListBySort[j] = TempListBySort[j + 1];
                                            TempListBySort[j + 1] = temp;
                                        }
                                    }
                                }
                                foreach (var music in TempListBySort)
                                {
                                    MusicRecords.Add(music);
                                }
                                this.ShowAllGrid.ItemsSource = MusicRecords;
                            }
                            else
                            {
                                MessageBox.Show("Please,enter genre  ", " ", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        break;
                }
            }
            else
            {
                MessageBox.Show("Please, choose what to search for", " ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

       
        #endregion
        //_________________________________________________ ! MOST POPULAR ! _________________________________________________\\





    }
}
