using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace HostApp.Startup;

public class HostWpf : IHostedService
{
    // public Application app { get; set; }

    public WpfContext WpfContext { get; set; }

    public HostWpf(WpfContext wpfContext, IHostApplicationLifetime lifetime)
    {
        this.WpfContext = wpfContext;
        lifetime.ApplicationStarted.Register(() => { this.WpfContext.Flag.Set(); });
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        this.WpfContext.PreUiThreadStart();
        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await this.WpfContext.Dispatcher.InvokeAsync(() =>
        {
            this.WpfContext.Application.Shutdown();
        });
    }
}