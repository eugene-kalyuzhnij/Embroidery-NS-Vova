using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NSEmbroidery.UI.Embroidery;

namespace NSEmbroidery.UI
{
    public partial class AddressOfService : Form
    {
        public AddressOfService()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var service = EmbroideryService.GetEmbroideryService(textBoxUri.Text);
                service.Endpoint.Address = new System.ServiceModel.EndpointAddress(textBoxUri.Text);
                if (service.Endpoint.Contract.ContractType != typeof(IEmbroideryCreatorService))
                {
                    MessageBox.Show("The address is wrong");
                    return;
                }
                Properties.Settings.Default.AddressOfService = textBoxUri.Text;
                Properties.Settings.Default.Save();
                MainForm.labelAddress.Text = textBoxUri.Text;
                MainForm.labelAddress.Refresh();
            }
            catch (UriFormatException ex)
            {
                MessageBox.Show("Uri string is wrong");
                return;
            }

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
