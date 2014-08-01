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

namespace NSEmbroidery.WPFClient
{
    /// <summary>
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Users : Page
    {

        public Frame UsersContent { get; set; }

        public Users()
        {
            InitializeComponent();
        }

        private void Users_Loaded(object sender, RoutedEventArgs e)
        {
            NSEmbroideryClient client = NSEmbroideryClient.GetNSEmbroideryClient();
            List<User> users = client.GetUsers().Where(u => u.Id != client.CurrentUserId).ToList<User>();

            foreach (var user in users)
            {
                Button userButton = new Button()
                {
                    Margin = new Thickness(5, 2, 5, 2),
                    Content = user,
                };

                userButton.Click += userButton_Click;

                stackPanelUsers.Children.Add(userButton);
            }
        }

        void userButton_Click(object sender, RoutedEventArgs e)
        {
            Button otherUserButton = e.Source as Button;

            if (otherUserButton != null)
            {
                User user = otherUserButton.Content as User;
                if (user != null)
                {
                    UsersContent.NavigationService.Navigate(new OtherUser(user));
                }
            }
        }

    }
}
