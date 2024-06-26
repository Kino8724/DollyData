﻿using DollyData.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace DollyData
{

    public sealed partial class MainPage : Page
    {
        public ObservableCollection<Doll> Dolls { get; } = new ObservableCollection<Doll>();
        public ObservableCollection<Company> Companies { get; } = new ObservableCollection<Company>();
        public ObservableCollection<Doll> SingleDoll { get; } = new ObservableCollection<Doll>();
        public MainPage()
        {
            this.InitializeComponent();

            Dolls = new ObservableCollection<Doll>(DataAccess.GetAllDollData());
            Companies = new ObservableCollection<Company>(DataAccess.GetAllCompanyData());
            for (int i = 0; i < Companies.Count; i++)
            {
                Debug.WriteLine(Companies[i].Id + " " + Companies[i].Name);
            }
        }

        private void DollCard_ItemClick(object sender, ItemClickEventArgs e)
        {
            var Doll = (Doll)e.ClickedItem;
            SingleDoll.Clear();
            SingleDoll.Add(Doll);
        }

        private void  AddDoll_Click(object sender, RoutedEventArgs e)
        {
            AddDollPopup.IsOpen = true;
        }

        private void SubmitAddDoll_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = false;
            if(AddDollName.Text == "" || AddDollName.Text == null)
            {
                isValid = false;
            }
            else if(AddDollDescription.Text == "" || AddDollDescription.Text == null)
            {
                isValid = false;
            }
            else if(AddDollCompany.SelectionBoxItem == null)
            {
                isValid = false;
            }
            else if(AddDollLine.Text == "" || AddDollLine.Text == null)
            {
                isValid = false;
            }
            else if(AddDollAmount.Text == "" || AddDollAmount.Text == null || AddDollAmount.Text == "0")
            {
                isValid = false;
            }
            else
            {
                isValid = true;
            }

            if (isValid)
            {
            string name = AddDollName.Text.ToString();
            string description = AddDollDescription.Text.ToString();
            string image = "Assets\\Square44x44Logo.targetsize-256.png";
            int amount = int.Parse(AddDollAmount.Text.ToString());
            Debug.WriteLine("SELECTED VALUE: " + AddDollCompany.SelectedValue);
            Debug.WriteLine("SELECTED ITEM: " + AddDollCompany.SelectedItem);
            Debug.WriteLine("SELECTED INDEX: " + AddDollCompany.SelectedIndex);
            Debug.WriteLine("SELECTED BOXITEM: " + AddDollCompany.SelectionBoxItem);
            Debug.WriteLine("COMBOBOX TEXT: " + AddDollCompany.Text);
            int company = int.Parse(AddDollCompany.SelectedIndex.ToString()) + 1;
            string line = AddDollLine.Text.ToString();
            bool isFavorite = (bool)AddDollIsFavorite.IsChecked;

            AddDollPopup.IsOpen = false;
            Doll newDoll = DataAccess.AddDollData(name, description, line, amount, image, isFavorite, company);
            Debug.WriteLine("NEW DOLL AFTER QUERY " + newDoll.Id + " " + newDoll.Name + " CompanyId: " + newDoll.CompanyId);
            Dolls.Add(newDoll);
            
            AddDollName.Text = "";
            AddDollDescription.Text = "";
            AddDollCompany.Text = "";
            AddDollAmount.Text = "";
            AddDollLine.Text = "";
            AddDollIsFavorite.IsChecked = false;
            }
        }

        private void AddDollInt_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void RemoveDoll_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            for (int i = 0; i < Dolls.Count; i++)
            {
                if (Dolls[i].Id == (int)btn.Tag)
                {
                    Dolls.RemoveAt(i);
                }

            }
            SingleDoll.Clear();
        }

        private void AddCompany_Click(object sender, RoutedEventArgs e)
        {
            AddCompanyPopup.IsOpen = true;
        }

        private void SubmitAddCompany_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = false;
            if (AddCompanyName.Text == "" || AddCompanyName.Text == null)
            {
                isValid = false; 
            }
            else
            {
                isValid = true;
            }

            if (isValid)
            {
            string name = AddCompanyName.Text.ToString();

            AddCompanyPopup.IsOpen = false;
            Company newCompany = DataAccess.AddCompanyData(name);
            Debug.WriteLine("NEW COMPANY AFTER QUERY " + newCompany.Id + " " + newCompany.Name);
            Companies.Add(newCompany);
            Debug.WriteLine(Companies.Last().Name);

            AddCompanyName.Text = "";
            }
        }
    }
}
