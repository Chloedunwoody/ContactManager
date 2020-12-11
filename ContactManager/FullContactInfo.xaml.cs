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
    /// Interaction logic for FullContactInfo.xaml
    /// </summary>
    public partial class FullContactInfo : Window
    {
        ObservableCollection<Person> contactsList = new ObservableCollection<Person>();
        DBHandler db = DBHandler.Instance;
        

        public FullContactInfo(List<Person> People)
        {
            InitializeComponent();
            List<Person> people = new List<Person>();
            foreach (var p in People)
            {
                people.Add(p);
            }
            
            foreach (var person in people)
            {
                contactsList.Add(person);
            }
            lvDataBinding.ItemsSource = contactsList;
        }

        private void lvDataBinding_Loaded(object sender, RoutedEventArgs e)
        {
           //??
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

        private void lvDataBinding_MouseEnter(object sender, MouseEventArgs e)
        {
            lvDataBinding.Foreground = Brushes.Blue;
        }


        private void lvDataBinding_MouseLeave(object sender, MouseEventArgs e)
        {
            //if(lvDataBinding.Focus())           
            //    lvDataBinding.Foreground = Brushes.Blue;           
            //else
            // Not sure about that
            lvDataBinding.Foreground = Brushes.White;
        }
    }
}

