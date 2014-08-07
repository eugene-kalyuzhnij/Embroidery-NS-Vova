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
using NSEmbroidery.Data.Models;

using System.Net.Http;
using System.Net.Http.Headers;

namespace NSEmbroidery.WPFClient
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public MainWindow MainWindow { get; set; }

        public Login(MainWindow mainWindow)
        {
            InitializeComponent();
            MainWindow = mainWindow;
        }

        private void logIn_Click(object sender, RoutedEventArgs e)
        {
           
            string _email = email.Text;
            string _password = password.Text;

            

            LoginModel model = null;

            model = new LoginModel()
            {
                Email = (_email != "")? _email : "test@mail.com",
                Password = (_password != "")? _password : "111111"
            };
            
            NSEmbroideryClient client = NSEmbroideryClient.GetNSEmbroideryClient();
            if (client.Login(model))
            {
                if (MainWindow != null)
                    MainWindow.mainFrame.NavigationService.Navigate(new Menu(MainWindow));
                else throw new NotImplementedException();
            }

        }
    }
}
