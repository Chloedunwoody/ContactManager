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
        DBHandler db = new DBHandler();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void WrapPanel_Loaded(object sender, RoutedEventArgs e)
        {        
            List<Person> Peoplpe = db.ReadAllPersons();
            foreach (var person in Peoplpe) 
            { 
                contactsList.Add(person);
            }
            lvDataBinding.ItemsSource = contactsList;
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            
            AddPerson addPerson = new AddPerson();
            addPerson.Show();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            int pId = -1;
            foreach (object o in lvDataBinding.SelectedItems)
            {
                pId = (lvDataBinding.SelectedItem as Person).Id;
            }
            UpadatePerson upadatePerson = new UpadatePerson(pId);
            upadatePerson.Show();

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
        
    }       
}
