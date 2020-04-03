using Holdings.Core.Models;
using Holdings.Core.Repositories.Contracts;
using Holdings.Core.Services.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Holdings.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> Create(User user)
        {
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CommitAsync();
            return user;
        }

        public async Task Update(User userToBeUpdated, User user)
        {
            userToBeUpdated.PasswordHash = user.PasswordHash;
            userToBeUpdated.PasswordSalt = user.PasswordSalt;

            await _unitOfWork.CommitAsync();
        }

        public async Task Delete(User user)
        {
            _unitOfWork.Users.Remove(user);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _unitOfWork.Users.GetAllAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _unitOfWork.Users.SingleOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _unitOfWork.Users.GetByUsernameAsync(username);
        }
    }
}