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
    /// Interaction logic for OtherUser.xaml
    /// </summary>
    public partial class OtherUser : Page
    {
        public Frame UsersContent { get; private set; }
        public User User { get; private set; }

        public OtherUser(User user, Frame usersContent)
        {
            InitializeComponent();
            User = user;
            UsersContent = usersContent;
        }


        private void OtherUser_Loaded(object sender, RoutedEventArgs e)
        {          
            //frameOtherUserContent
            frameOtherUserContent.NavigationService.Navigate(new Gallery(User.Id));
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            if (UsersContent != null)
                UsersContent.NavigationService.Navigate(new Users(UsersContent));
            else
                throw new NotImplementedException("In Other User: UserContent is null");
        }


    }
}
