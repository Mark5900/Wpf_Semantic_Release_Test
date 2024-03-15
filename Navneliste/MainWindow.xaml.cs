using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Navneliste
{
    public partial class MainWindow : Window
    {
        bool bTest = true;
        int iMaxNamesCount = 10;
        List<string> lNames = new List<string>();

        public MainWindow()
        {
            InitializeComponent();

            if (bTest)
            {
                UseTestData();
            }

            lstNames.Items.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));
        }

        private void btnRemovePosition_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPosition.Text))
            {
                MessageBox.Show("Indtast venligst en position");
            } else if (string.IsNullOrWhiteSpace(txtPosition.Text))
            {
                MessageBox.Show("Indtast venligst en position");
            } else if (!int.TryParse(txtPosition.Text, out _))
            {
                MessageBox.Show("Positionen skal være et tal");
            } else
            {
                int iPosition = int.Parse(txtPosition.Text.Trim());
                if (iPosition >= 0 && iPosition <= lNames.Count - 1)
                {
                    string sName = lstNames.Items[iPosition].ToString();
                    lNames.Remove(sName);

                    SetListBoxItems();
                } else
                {
                    MessageBox.Show("Positionen findes ikke");
                }
            }
        }

        private void btnAddName_Click(object sender, RoutedEventArgs e)
        {
            if (lNames.Count < iMaxNamesCount) {
                if (string.IsNullOrEmpty(txtName.Text))
                {
                    MessageBox.Show("Indtast venligst et navn");
                } else if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Indtast venligst et navn");
                } else
                {
                    lNames.Add(txtName.Text);
                    txtName.Text = "";
                    SetListBoxItems();
                }
            } else
            {
                MessageBox.Show($"Der kan maksimalt være {iMaxNamesCount} navne på listen");
            }
        }

        private void btnSortAscending_Click(object sender, RoutedEventArgs e)
        {
            lstNames.Items.SortDescriptions.Clear();
            lstNames.Items.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));
        }

        private void btnSortDescending_Click(object sender, RoutedEventArgs e)
        {
            lstNames.Items.SortDescriptions.Clear();
            lstNames.Items.SortDescriptions.Add(new SortDescription("", ListSortDirection.Descending));
        }

        private void btnRemoveSelectedName_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lstNames.SelectedItem != null)
                {
                    lNames.Remove(lstNames.SelectedItem.ToString());

                    SetListBoxItems();
                }
            }
            catch (Exception)
            {
                // Do nothing if no item is selected
            }
        }

        private void UseTestData()
        {
            string[] aNames = { "FC København", "Brøndby IF", "AGF", "FC Midtjylland", "OB", "AaB", "Randers FC", "SønderjyskE", "Vejle BK", "Lyngby BK" };

            foreach (string aName in aNames)
            {
                lNames.Add(aName);
            }

            SetListBoxItems();
        }

        private void SetListBoxItems()
        {
            lstNames.ItemsSource = null;
            lstNames.ItemsSource = lNames;
        }
    }
}