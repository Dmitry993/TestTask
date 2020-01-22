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
        ApplicationUser Get(int id);
        void CreateUser(ApplicationUser item);
        Task<bool> UserExist(string id);
    }
}
