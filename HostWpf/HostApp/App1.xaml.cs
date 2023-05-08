using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.Hosting;

namespace HostApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App1 : Application
    {
        public IHostApplicationLifetime Lifetime { get; set; }

        public App1(MainWindow mainWindow, IHostApplicationLifetime lifetime)
        {
            Lifetime = lifetime;
            this.MainWindow = mainWindow;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this.MainWindow.Show();
        }


        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            Debug.WriteLine("exit");
            Lifetime.StopApplication();
        }
    }
}