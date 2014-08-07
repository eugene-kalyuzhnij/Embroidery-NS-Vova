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
        public MainWindow MainWindow { get; private set; }

        public static Frame Content { get; set; }

        public Menu(MainWindow mainWindow)
        {
            InitializeComponent();
            MainWindow = mainWindow;

            Content = content;
        }
            
        private void MyGallery_Click(object sender, RoutedEventArgs e)
        {
            content.NavigationService.Navigate(new Gallery(content));
        }

        private void Users_Click(object sender, RoutedEventArgs e)
        {
            content.NavigationService.Navigate(new UsersContent());
        }

        private void AddEmbroidery_Click(object sender, RoutedEventArgs e)
        {
            content.NavigationService.Navigate(new AddEmbroidery(content));
        }

        private void Menu_Loaded(object sender, RoutedEventArgs e)
        {
            NSEmbroideryClient client = NSEmbroideryClient.GetNSEmbroideryClient();
            userName.Text = client.CurrentUserName;
        }

        private void Logoff_Click(object sender, RoutedEventArgs e)
        {
            NSEmbroideryClient client = NSEmbroideryClient.GetNSEmbroideryClient();
            if (client.Logoff())
                this.NavigationService.Navigate(new Login(MainWindow));
            else
                MessageBox.Show("Some error was occured");
        }
    }
}
