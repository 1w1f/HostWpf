using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using HostApp;
using HostApp.Startup;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var host = Host.CreateDefaultBuilder().ConfigureServices((context, service) =>
    {
        service.AddSingleton<Application, App1>();
        service.AddSingleton<WpfContext>();
        service.AddSingleton<MainWindow>();
        service.AddHostedService<HostWpf>();
    })
    .Build();
var lifeTime = host.Services.GetService<IHostApplicationLifetime>();

lifeTime.ApplicationStarted.Register(() => { Debug.WriteLine("host started"); });
lifeTime.ApplicationStopped.Register(() => { Debug.WriteLine("host ended"); });
await host.RunAsync();
Console.WriteLine("hello");
Debug.WriteLine("hello world");