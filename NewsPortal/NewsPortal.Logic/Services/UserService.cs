using AutoMapper;
using NewsPortal.Data.Model;
using NewsPortal.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationUser = NewsPortal.Logic.Model.ApplicationUser;

namespace NewsPortal.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<User> _repository;         

        public UserService(IMapper mapper, IRepository<User> repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void CreateUser(ApplicationUser item)
        {
            var result = _mapper.Map<Data.Model.User>(item);
            _repository.Create(result);
            _repository.Save();
        }

        public async Task<bool> UserExist(string id)
        {
            var users = await _repository.GetAll();            
            var mapUsersToList = _mapper.Map<IEnumerable<User>, IEnumerable<ApplicationUser>>(users);
            var result = mapUsersToList.FirstOrDefault(b => b.GoogleId == id);

            if (result == null)
            {
                return false;
            }
            return true;
        }

        public Model.ApplicationUser Get(int id)
        {
            var user = _repository.Get(id);
            return _mapper.Map<Model.ApplicationUser>(user);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUser()
        {
            var usersList = await _repository.GetAll();
            return _mapper.Map<IEnumerable<User>, IEnumerable<ApplicationUser>>(usersList);
        }
    }
}
