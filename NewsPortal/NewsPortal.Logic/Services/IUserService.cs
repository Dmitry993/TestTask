using NewsPortal.Logic.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Logic.Services
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser> GetUserAsync(int id);
        Task<int> CreateUserAsync(ApplicationUser item);
        Task<int> GetOrCreateUserAsync(ApplicationUser item);       
    }
}
