using AutoMapper;
using NewsPortal.Data.Model;
using NewsPortal.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsPortal.Logic.Model;

namespace NewsPortal.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;         

        public UserService(IMapper mapper, IUserRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<string> CreateUserAsync(ApplicationUser item)
        {
            var mapUser = _mapper.Map<Data.Model.User>(item);
            await _repository.CreateAsync(mapUser);
            await _repository.SaveAsync();

            var users = await _repository.GetAllAsync();
            var mapUsers = _mapper.Map<IEnumerable<User>, IEnumerable<ApplicationUser>>(users);
            var result = mapUsers.FirstOrDefault(b => b.GoogleId == item.GoogleId);
                       
            return result.Id.ToString();
        }

        public async Task<string> GetUserIdAsync(ApplicationUser item)
        {            
            var existUser = await _repository.FindUserByGoogleIdAsync(item.GoogleId);

            if (existUser == null)
            {
                var user = _mapper.Map<User>(item);
                await _repository.CreateAsync(user);
                await _repository.SaveAsync();                
                return user.Id.ToString();
            }

            return existUser.Id.ToString();
        }              

        public async Task<ApplicationUser> GetUserAsync(int id)
        {
            var user = await _repository.GetAsync(id);
            return _mapper.Map<ApplicationUser>(user);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUserAsync()
        {
            var usersList = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<User>, IEnumerable<ApplicationUser>>(usersList);
        }
    }
}
