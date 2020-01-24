using AutoMapper;
using NewsPortal.Data.Model;
using NewsPortal.Data.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsPortal.Logic.Model;
using System.Threading;

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

        public async Task<int> CreateUserAsync(ApplicationUser applicationUser)
        {
            var user = _mapper.Map<User>(applicationUser);
            await _repository.CreateAsync(user);
            await _repository.SaveAsync();
            return user.Id;
        }

        public async Task<int> GetOrCreateUserAsync(ApplicationUser applicationUser)
        {            
            var user = await _repository.FindUserByGoogleIdAsync(applicationUser.GoogleId);

            if (user == null)
            {                
                return await CreateUserAsync(applicationUser);             
            }
                        
            return user.Id;
        }              

        public async Task<ApplicationUser> GetUserAsync(int id)
        {
            var user = await _repository.GetAsync(id);
            return _mapper.Map<ApplicationUser>(user);
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            var usersList = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<User>, IEnumerable<ApplicationUser>>(usersList);
        }
    }
}
