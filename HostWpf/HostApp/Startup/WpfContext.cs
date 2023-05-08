using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace HostApp.Startup;

public class WpfContext
{
    public ManualResetEvent Flag { get; set; } = new ManualResetEvent(false);
    public Application Application { get; set; }

    public Thread WpfThread { get; set; }

    public IServiceProvider ServiceProvider { get; set; }
    public Dispatcher Dispatcher => this.Application.Dispatcher;

    public WpfContext(IServiceProvider serviceProvider)
    {
        this.ServiceProvider = serviceProvider;

    }

    public void PreUiThreadStart()
    {
        this.WpfThread = new Thread(UiThreadStart);
        WpfThread.SetApartmentState(ApartmentState.STA);
        WpfThread.Start();
    }


    public void UiThreadStart()
    {
        this.Application = this.ServiceProvider.GetService<Application>();
        this.Dispatcher.Invoke(() =>
        {
            var appType = this.Application is App1;
            Debug.WriteLine("application create by STA Thread and context start and wait host start");
            Flag.WaitOne();
            Application.Run();
        });
    }
}