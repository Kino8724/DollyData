using DollyData.Models;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

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
            string image = "Assets\\Square44x44Logo.targetsize-256.png";
            int amount = int.Parse(AddDollAmount.Text.ToString());
            int company = int.Parse(AddDollCompany.Text.ToString());
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
                if (Dolls[i].Id == (int)btn.Tag)
                {
                    Dolls.RemoveAt(i);
                }

            }
            SingleDoll.Clear();
        }
    }
}
