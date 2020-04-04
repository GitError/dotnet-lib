using Holdings.Core.Models;
using Holdings.Core.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Holdings.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private HoldingsDbContext _context => _context as HoldingsDbContext;

        public UserRepository(HoldingsDbContext context)
            : base(context)
        { }
        
        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Username.Contains(username));
        }
    }
}