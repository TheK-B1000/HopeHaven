using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Collaborative_Resource_Management_System.Services;
using Collaborative_Resource_Management_System.Models.Interfaces;
using System.Linq;

public class WeeklyEmailService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public WeeklyEmailService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await DoWork();
            await Task.Delay(TimeSpan.FromDays(7), stoppingToken);
        }
    }

    private async Task DoWork()
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var inventoryService = scope.ServiceProvider.GetRequiredService<IInventoryService>();
            var lowStockItems = await inventoryService.GetLowStockItemsAsync();
            if (lowStockItems.Any())
            {
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
                await emailService.SendLowStockAlert(lowStockItems);
            }
        }
    }
}
