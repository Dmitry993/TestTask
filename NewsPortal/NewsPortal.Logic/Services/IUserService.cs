using NewsPortal.Logic.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Logic.Services
{
    public interface IUserService
    {
        Task<IEnumerable<ApplicationUser>> GetAllUser();
        ApplicationUser GetUser(int id);
        Task<string> CreateUser(ApplicationUser item);
        Task<string> FindUserByGoogleId(string id);
    }
}
