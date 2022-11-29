using ProjectCodeX.Models;

namespace ProjectCodeX.Services
{
    public class DuesService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DuesService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var _dbContext = scope.ServiceProvider.GetService<ProjectCodeXContext>();
                List<User> usersWhoShouldBeBilledThisMonth = _dbContext.Users.Where(u => u.NextBillDate.Value.Month == DateTime.Now.Month && u.NextBillDate.Value.Day == DateTime.Now.Day).ToList();
                if (usersWhoShouldBeBilledThisMonth.Count > 0)
                {
                    foreach (var user in usersWhoShouldBeBilledThisMonth)
                    {
                        user.Balance += user.DueTier switch
                        {
                            0 => 5,
                            1 => 10,
                            2 => 20,
                            3 => 50,
                            4 => 100,
                            _ => 0
                        };

                        user.NextBillDate = DateTime.Now.AddDays(30);
                    }
                    _dbContext.SaveChanges();
                }
                
                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}
