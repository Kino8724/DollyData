using DollyData.Models;
using DollyData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Foundation.Metadata;
using Windows.System;
using Windows.Media.Casting;
using System.Data.SqlTypes;

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
            string name = AddDollName.Text.ToString();
            string description = AddDollDescription.Text.ToString();
            string image = "Assets/Square44x44Logo.targetsize-24_altform-unplated.png";
            int amount = int.Parse(AddDollAmount.Text.ToString());
            Guid company = Guid.NewGuid();
            string line = AddDollLine.Text.ToString();
            bool isFavorite = (bool)AddDollIsFavorite.IsChecked;

            AddDollPopup.IsOpen = false;
            Doll newDoll = new Doll(name, description, line, amount, image, isFavorite, company);
            Dolls.Add(newDoll);

            AddDollName.Text = "";
            AddDollDescription.Text = "";
            AddDollCompany.Text = "";
            AddDollAmount.Text = "";
            AddDollLine.Text = "";
            AddDollIsFavorite.IsChecked = false;
        }

        private void AddDollAmount_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            args.Cancel = args.NewText.Any(c => !char.IsDigit(c));
        }

        private void RemoveDoll_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            for (int i = 0; i < Dolls.Count; i++)
            {
                if (Dolls[i].id == (Guid)btn.Tag)
                {
                    Dolls.RemoveAt(i);
                }

            }
            SingleDoll.Clear();
        }
    }
}
