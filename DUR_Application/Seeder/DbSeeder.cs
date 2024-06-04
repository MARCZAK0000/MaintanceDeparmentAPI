using DUR_Application.Entities;

namespace DUR_Application.Seeder
{
    public class DbSeeder:DatabasePrimarySeed
    {
        private readonly DatabaseContext _dbContext;
        public DbSeeder(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if(!_dbContext.Roles.Any())
                {
                    var role = GetRoles();
                    await _dbContext.AddRangeAsync(role);
                    await _dbContext.SaveChangesAsync();
                }

                if(!_dbContext.Lanes.Any())
                {
                    var lanes = GetLanes();
                    await _dbContext.AddRangeAsync(lanes);
                    await _dbContext.SaveChangesAsync();
                }


                if (!_dbContext.Users.Any())
                {
                    var user = GetAdmin();
                    await _dbContext.AddRangeAsync(user);
                    await _dbContext.SaveChangesAsync();
                }

                if(!_dbContext.Magazines.Any())
                {
                    var magazine = GetMagazine(); 
                    await _dbContext.AddRangeAsync(magazine);
                    await _dbContext.SaveChangesAsync();
                }

                if (!_dbContext.RequestTypes.Any())
                {
                    var requestTypes = GetRequestTypes();
                    await _dbContext.AddRangeAsync(requestTypes);
                    await _dbContext.SaveChangesAsync();
                }

                if (!_dbContext.RequestStatus.Any())
                {
                    var requestStatus = GetRequestStatus();
                    await _dbContext.AddRangeAsync(requestStatus);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
