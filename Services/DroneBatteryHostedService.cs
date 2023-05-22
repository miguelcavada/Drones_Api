using Drones_Api.Data;
using Drones_Api.Repository.IRepository;

namespace DronesAPI.Services
{
    public class DroneBatteryHostedService : IHostedService, IDisposable
    {
        private Timer? _timer;
        public IServiceProvider Service { get; private set; }

        public DroneBatteryHostedService(IServiceProvider service)
        {
            Service = service;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
            return Task.CompletedTask;
        }

        private async void DoWork(object? state)
        {
            using (var scope = Service.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DronesDB>();

                var dronesBatteryLevels = context.Drones.Select(x => new { x.Id, x.BatteryCapacity });
                foreach (var item in dronesBatteryLevels)
                {
                    context.DroneLogs.Add(new DroneLogs
                    {
                        DroneId = item.Id,
                        BatteryLevel = item.BatteryCapacity,
                        CreatedDate = DateTime.Now
                    });
                }
                await context.SaveChangesAsync();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose() => _timer?.Dispose();
    }
}
