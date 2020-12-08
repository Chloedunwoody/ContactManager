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
    /// Interaction logic for AddPerson.xaml
    /// </summary>
    public partial class AddPerson : Window
    {
        DBHandler db = DBHandler.Instance;

        public AddPerson()
        {
            InitializeComponent();
            
        }
        private void SavePersonButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
