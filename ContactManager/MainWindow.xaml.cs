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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ContactManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Person> contactsList = new ObservableCollection<Person>();
        DBHandler db = DBHandler.Instance;
        //private int lastSelectedChoice = -1;


        public MainWindow()
        {
            InitializeComponent();
            //lastSelectedChoice = contactsList.Sele
            MouseDoubleClick += MainWindow_MouseDoubleClick;
            //MouseLeftButtonDown += MainWindow_MouseLeftButtonDown;

        }

        //private void MainWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    int pId = -1;
        //    foreach (object o in lvDataBinding.SelectedItems)
        //    {
        //        pId = (lvDataBinding.SelectedItem as Person).Id;
        //    }
        //    lastSelectedChoice = pId;
        //}

        private void MainWindow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int pId = -1;
            foreach (object o in lvDataBinding.SelectedItems)
            {
                pId = (lvDataBinding.SelectedItem as Person).Id;
            }
            if (pId != -1)
            {
                UpadatePerson upadatePerson = new UpadatePerson(pId);
                upadatePerson.ShowDialog();
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
            if(lvDataBinding.SelectedItem != null)
            {
                UpadatePerson upadatePerson = new UpadatePerson((lvDataBinding.SelectedItem as Person).Id);
                upadatePerson.ShowDialog();
            }
            else 
            {
                MessageBox.Show("Chose a person to update","Warning!", MessageBoxButton.OK);
            }
            UpdateList();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (lvDataBinding.SelectedItem != null)
            {
                var person = lvDataBinding.SelectedItem as Person;
                MessageBoxResult result = MessageBox.Show("Are you sure you want delete this contact?", "Warning!", MessageBoxButton.YesNoCancel);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        db.DeletePerson(person.Id);
                        break;
                    default:
                        break;
                }
            }          
            else
            {
                MessageBox.Show("Chose a person to update", "Warning!", MessageBoxButton.OK);
            }
            UpdateList();
        }

        public void UpdateList()
        {
            lvDataBinding.ItemsSource = contactsList;
            contactsList.Clear();
            List<Person> Peoplpe = db.ReadAllPersons();
            foreach (var person in Peoplpe)
            {
                contactsList.Add(person);
            }
            lvDataBinding.ItemsSource = contactsList;
        }
    }       
}
