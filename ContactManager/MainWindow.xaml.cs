using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;

namespace ContactManager
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Person> contactsList = new ObservableCollection<Person>();
        ObservableCollection<Person> InnerContactsList = new ObservableCollection<Person>();
        DBHandler db = DBHandler.Instance;



        public MainWindow()
        {
            InitializeComponent();
            MouseDoubleClick += MainWindow_MouseDoubleClick;
        }

        private void ExpanderMain_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //int getPersonId = -1;
            //if (lvDataBinding.SelectedItem != null)
            //{

            //    getPersonId = (lvDataBinding.SelectedItem as Person).Id;
            //    Person person = db.GetPerson(getPersonId);

            //    person = IdTextBox.Text;



            //}
        }

        private void MainWindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            List<Person> people = new List<Person>();
            if (lvDataBinding.SelectedItem != null)
            {
                foreach (object o in lvDataBinding.SelectedItems)
                    people.Add(o as Person);

                FullContactInfo fullContact = new FullContactInfo(people);
                fullContact.ShowDialog();
            }
        }

        private void Data_Loader(object sender, RoutedEventArgs e)
        {
            UpdateList();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddPerson addPerson = new AddPerson();
            addPerson.ShowDialog();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (lvDataBinding.SelectedItem != null)
            {
                UpadatePerson upadatePerson = new UpadatePerson((lvDataBinding.SelectedItem as Person).Id);
                upadatePerson.ShowDialog();
            }
            else
            {
                MessageBox.Show("Chose a person to update", "Warning!", MessageBoxButton.OK);
            }
            UpdateList();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (lvDataBinding.SelectedItem != null)
            {

                List<Person> people = new List<Person>();

                foreach (object o in lvDataBinding.SelectedItems)
                { people.Add(o as Person); }

                string listOfIds = "";

                foreach (var p in people)
                {
                    if (listOfIds.Length > 0)
                    {
                        listOfIds += ",";
                    }
                    listOfIds += "'";
                    listOfIds += p.Id;
                    listOfIds += "'";

                }

                MessageBoxResult result = MessageBox.Show("Are you sure you want delete this contact?", "Warning!", MessageBoxButton.YesNoCancel);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        db.DeletePerson(listOfIds);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Chose a person to delete", "Warning!", MessageBoxButton.OK);
            }
            UpdateList();
        }

        public void UpdateList()
        {
            lvDataBinding.ItemsSource = contactsList;
            contactsList.Clear();
            List<Person> People = db.ReadAllPersons();
            foreach (var person in People)
            {
                contactsList.Add(person);
            }
            lvDataBinding.ItemsSource = contactsList;
            
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            DBHandler db = DBHandler.Instance;
            char[] delimeterChar = { ';', ',' };
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV file (*.csv)|*.csv|All files(*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (openFileDialog.ShowDialog() == true)
            {
                int nbLines = File.ReadAllLines(openFileDialog.FileName).Length;
                foreach (var line in File.ReadAllLines(openFileDialog.FileName))
                {
                    MessageBox.Show(line.ToString());
                    string[] tempPerson = line.Split(delimeterChar);

                    Person newPerson = new Person(tempPerson[0], tempPerson[1], tempPerson[2], tempPerson[3]);
                    db.AddPerson(newPerson);
                    UpdateList();
                }
                

            }

        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            string csvInput;
            string header = "ID,First Name,Last Name,Email,Phone\n";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV file (*.csv)|*.csv|Text file (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, header);
                foreach (Person p in contactsList)
                {
                    csvInput = string.Format("{0},{1},{2},{3}\n", p.Id, p.FirstName,p.LastName,p.Email,p.Phone);
                    File.AppendAllText(saveFileDialog.FileName,csvInput);
                    
                }
                MessageBox.Show("Export Successful", "Success!", MessageBoxButton.OK);

            }

        }

        private void AddButton_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = AddButton;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Add a Contact";
        }

        private void AddButton_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void UpdateButton_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = UpdateButton;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Update a Contact";

        }

        private void UpdateButton_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }
        private void DeleteButton_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = DeleteButton;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Delete a Contact";
        }

        private void DeleteButton_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void ImportButton_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = ImportButton;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Import Contacts"; 
        }

        private void ImportButton_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void ExportButton_MouseEnter(object sender, MouseEventArgs e)
        {
            popup_uc.PlacementTarget = ExportButton;
            popup_uc.Placement = PlacementMode.Right;
            popup_uc.IsOpen = true;
            Header.PopupText.Text = "Export Contacts"; 
        }

        private void ExportButton_MouseLeave(object sender, MouseEventArgs e)
        {
            popup_uc.Visibility = Visibility.Collapsed;
            popup_uc.IsOpen = false;
        }

        private void PowerOff_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
            
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

    }

}
     
