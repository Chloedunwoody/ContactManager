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
    /// Interaction logic for UpadatePerson.xaml
    /// </summary>
    public partial class UpadatePerson : Window
    {
        ObservableCollection<Person> contactsList = new ObservableCollection<Person>();
        private int personId;
        DBHandler db = new DBHandler();
       

        public UpadatePerson(int PersonId)
        {           
            InitializeComponent();
            personId = PersonId;
        }
        private void SavePersonButton_Click(object sender, RoutedEventArgs e)
        {
            Person updatedPerson = new Person();
            TextBox firstNameBox = FirstNameBox;
            TextBox lastNameBox = LastNameBox;
            TextBox emailBox = EmailBox;
            TextBox phoneBox = PhoneBox;

            updatedPerson.FirstName = firstNameBox.ToString();
            updatedPerson.LastName = lastNameBox.ToString();
            updatedPerson.Email = emailBox.ToString();
            updatedPerson.Phone = phoneBox.ToString();
            db.UpdatePerson(updatedPerson);
        }

        private void ChosenPerson_Loaded(object sender, RoutedEventArgs e)
        {
            Person person = db.GetPerson(personId);
            contactsList.Add(person);
            lvDataBinding.ItemsSource = contactsList;
        }
    }
}
