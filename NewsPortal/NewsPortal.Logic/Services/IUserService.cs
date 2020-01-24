using NewsPortal.Logic.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsPortal.Logic.Services
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> GetAllUsersAsync();
        Task<ApplicationUser> GetUserAsync(int id);
        Task<ApplicationUser> CreateUserAsync(ApplicationUser item);
        Task<ApplicationUser> GetOrCreateUserAsync(ApplicationUser item);       
    }
}
