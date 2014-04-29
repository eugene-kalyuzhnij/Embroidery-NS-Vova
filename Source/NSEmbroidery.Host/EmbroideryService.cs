using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using NSEmbroidery.Core;
using System.Threading;
using System.IO;

namespace NSEmbroidery.Host
{
    public partial class EmbroideryService : ServiceBase
    {
        ServiceHost host = null;
        Thread hostThread;
        EventLog log = new EventLog("EmbroideryServiceLog");

        public EmbroideryService()
        {
            log.Source = ("EmbroiderySource");
            InitializeComponent();
            hostThread = new Thread(new ThreadStart(StartThread));
            
        }

        protected override void OnStart(string[] args)
        {
            this.RequestAdditionalTime(0);
            try
            {
                if (host != null)
                    host.Close();

                hostThread.Start();
            }
            catch (Exception e)
            {
                log.WriteEntry("Exception in OnStart: " + e.Message);
            }
        }


        public void StartThread()
        {
            try
            {
                host = new ServiceHost(typeof(EmbroideryCreator));
                host.Open();
            }
            catch (Exception e)
            {
                log.WriteEntry("Exception in StartThread: " + e.Message);
            }

            log.WriteEntry("Hosting has started");
        }

        protected override void OnStop()
        {
            try
            {
                if (host != null)
                {
                    host.Close();
                    host = null;
                }
            }
            catch (Exception e)
            {
                log.WriteEntry("Exception in OnStop " + e.Message);
            }

        }

    }
}
