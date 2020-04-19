using Domain;
using Domain.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.SqlServer
{
    public class UserErrorsRepository : IUserErrorsRepository
    {
        private UserPerformanceDbContext _userPerformanceDbContext;

        public UserErrorsRepository(UserPerformanceDbContext userPerformanceDbContext)
        {
            _userPerformanceDbContext = userPerformanceDbContext;
        }

        public async Task DeleteUserError(Guid userErrorId)
        {
            UserError toRemove = GetUserError(userErrorId);
            _userPerformanceDbContext.UserErrors.Remove(toRemove);
            await _userPerformanceDbContext.SaveChangesAsync();
        }

        public UserError GetUserError(Guid userErrorId)
        {
            return _userPerformanceDbContext.UserErrors.SingleOrDefault(x => x.UserErrorId == userErrorId);
        }

        public async Task SaveUserErrors(UserError userError)
        {
            await _userPerformanceDbContext.UserErrors.AddAsync(userError);
            await _userPerformanceDbContext.SaveChangesAsync();
        }

        public async Task UpdateUserError(UserError userError)
        {
            _userPerformanceDbContext.Update(userError);
            await _userPerformanceDbContext.SaveChangesAsync();
        }
    }
}
