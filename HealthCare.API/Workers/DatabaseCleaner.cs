namespace HealthCare.API.Workers
{
    using System;
    using System.Linq;
    using DataLayer;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class DatabaseCleaner : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DatabaseCleaner(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            await ClearAppointmentHoursTable(stoppingToken);
        }

        private async Task ClearAppointmentHoursTable(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<HealthCareDbContext>();

                var hours = await dbContext.AppointmentHours
                    .Where(x => x.EndDate < DateTime.Now.AddHours(-12))
                    .ToListAsync(stoppingToken);

                dbContext.RemoveRange(hours);

                await dbContext.SaveChangesAsync(stoppingToken);

                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
