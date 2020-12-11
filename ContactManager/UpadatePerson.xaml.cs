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
        Person person = new Person();
        private int personId;
        DBHandler db = DBHandler.Instance;

        
        public UpadatePerson(int PersonId)
        {           
            InitializeComponent();
            personId = PersonId;
        }
        private void SavePersonButton_Click(object sender, RoutedEventArgs e)
        {
            Person updatedPerson = new Person();
            updatedPerson.Id = personId;
            updatedPerson.FirstName = FirstNameBox.Text;
            updatedPerson.LastName = LastNameBox.Text;
            updatedPerson.Email = EmailBox.Text;
            updatedPerson.Phone = PhoneBox.Text;

            MessageBoxResult result = MessageBox.Show("Are you sure you want to save those changes?", "Warning!", MessageBoxButton.YesNoCancel);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    db.UpdatePerson(updatedPerson);
                    (Application.Current.MainWindow as MainWindow).UpdateList();
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Cancel:
                    this.Close();
                    break;
                default:
                    this.Close();
                    break;

            }           
        }

        private void ChosenPerson_Loaded(object sender, RoutedEventArgs e)
        {
            Person person = db.GetPerson(personId);
            FirstNameBox.Text = person.FirstName;
            LastNameBox.Text = person.LastName;
            EmailBox.Text = person.Email;
            PhoneBox.Text = person.Phone;
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
