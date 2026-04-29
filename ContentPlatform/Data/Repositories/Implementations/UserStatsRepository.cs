using ContentPlatform.Data.Repositories.Interfaces;
using ContentPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace ContentPlatform.Data.Repositories.Implementations
{
    public class UserStatsRepository(AppDbContext appDbContext): IUserStatsRepository
    {
        public async Task CreateAsync(UserStats userStats)
        {
            await appDbContext.UserStats.AddAsync(userStats);
            await appDbContext.SaveChangesAsync();
        }

        public async Task<UserStats> GetUserStatsAsync(int userId)
        {
            var userstats = await appDbContext.UserStats.FirstOrDefaultAsync(us => us.Id == userId);
            return userstats;
        }

        public async Task UpdateAsync(UserStats userStats)
        {
            appDbContext.UserStats.Update(userStats);
            await appDbContext.SaveChangesAsync();
        }
    }
}
