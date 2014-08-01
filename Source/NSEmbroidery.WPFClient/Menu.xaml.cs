using System;
using System.Collections.Generic;
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

namespace NSEmbroidery.WPFClient
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void MyGallery_Click(object sender, RoutedEventArgs e)
        {
            content.NavigationService.Navigate(new Gallery());
        }

        private void Users_Click(object sender, RoutedEventArgs e)
        {
            content.NavigationService.Navigate(new UsersContent());
        }

        private void AddEmbroidery_Click(object sender, RoutedEventArgs e)
        {
            content.NavigationService.Navigate(new AddEmbroidery());
        }
    }
}
