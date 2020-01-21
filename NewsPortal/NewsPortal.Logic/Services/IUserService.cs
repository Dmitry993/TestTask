using NewsPortal.Logic.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPortal.Logic.Services
{
    public interface IUserService
    {
        IEnumerable<ApplicationUser> GetAllUser();
        ApplicationUser Get(int id);
        void CreateUser(ApplicationUser item);
        bool UserExists(int id);
    }
}
