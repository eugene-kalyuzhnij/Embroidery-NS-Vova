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
using NSEmbroidery.UI.Properties;
using System.Configuration;
using System.ServiceModel;


namespace NSEmbroidery.UI
{
    public partial class AddressOfService : Form
    {
        public AddressOfService()
        {
            InitializeComponent();

            string[] keys = ConfigurationManager.AppSettings.AllKeys;
            
            comboBoxAddress.Items.AddRange(keys);

            comboBoxAddress.Text = Properties.Settings.Default.AddressOfService;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string currentUri;
                if (comboBoxAddress.SelectedItem != null)
                    currentUri = (string)comboBoxAddress.SelectedItem;
                else currentUri = comboBoxAddress.Text;
                var service = EmbroideryService.GetEmbroideryService(currentUri);
                service.PossibleResolutions(new Bitmap(1, 1), 1, 2, 3);
                if (service.Endpoint.Contract.ContractType != typeof(IEmbroideryCreatorService))
                {
                    MessageBox.Show("The address is wrong");
                    return;
                }
                Properties.Settings.Default.AddressOfService = currentUri;
                Properties.Settings.Default.Save();

                SaveAddress(currentUri);

                MainForm.labelAddress.Text = currentUri;
                MainForm.labelAddress.Refresh();
            }
            catch (UriFormatException ex)
            {
                MessageBox.Show("Uri string is wrong");
                return;
            }
            catch (EndpointNotFoundException ex)
            {
                MessageBox.Show("Endpoint wasn't founded. Try put another uri");
                return;
            }

            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void SaveAddress(string uri)
        {
            string[] allKeys = ConfigurationManager.AppSettings.AllKeys;

            foreach (string key in allKeys)
                if (key == uri) return;

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Add(uri, uri);
            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");

        }

        private void comboBoxAddress_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
