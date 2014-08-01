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
        public User User { get; private set; }

        public OtherUser(User user)
        {
            InitializeComponent();
            User = user;
        }

        private void OtherUser_Loaded(object sender, RoutedEventArgs e)
        {
            textBlockUserName.Text = User.ToString();
        }


    }
}
