using Holdings.Core.Models;
using Holdings.Core.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Holdings.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private HoldingsDbContext DbContext => Context as HoldingsDbContext;

        public UserRepository(HoldingsDbContext context)
            : base(context)
        { }
        
        public async Task<User> GetByUsernameAsync(string username)
        {
            return await DbContext.Users.SingleOrDefaultAsync(x => x.Username.Contains(username));
        }
    }
}