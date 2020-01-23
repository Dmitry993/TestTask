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

        public async Task<string> CreateUser(ApplicationUser item)
        {
            var mapUser = _mapper.Map<Data.Model.User>(item);
            _repository.Create(mapUser);
            await _repository.Save();

            var users = await _repository.GetAll();
            var mapUsers = _mapper.Map<IEnumerable<User>, IEnumerable<ApplicationUser>>(users);
            var result = mapUsers.FirstOrDefault(b => b.GoogleId == item.GoogleId);
                       
            return result.Id.ToString();
        }

        public async Task<string> FindUserByGoogleId(string id)
        {
            var users = await _repository.GetAll();            
            var mapUsers = _mapper.Map<IEnumerable<User>, IEnumerable<ApplicationUser>>(users);
            var result = mapUsers.FirstOrDefault(b => b.GoogleId == id);

            if (result == null)
            {
                return null;
            }
            return result.Id.ToString();
        }

        public Model.ApplicationUser GetUser(int id)
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
