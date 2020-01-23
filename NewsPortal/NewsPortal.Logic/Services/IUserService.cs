using NewsPortal.Logic.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Logic.Services
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> GetAllUserAsync();
        Task<ApplicationUser> GetUserAsync(int id);
        Task<string> CreateUserAsync(ApplicationUser item);
        Task<string> GetUserIdAsync(ApplicationUser item);       
    }
}
