using AutoMapper;
using NewsPortal.Data.Model;
using NewsPortal.Data.Repositories;
using NewsPortal.Logic.Model;
using System;
using System.Collections.Generic;

namespace NewsPortal.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<ApplicationUserDb> _repository;         

        public UserService(IMapper mapper, IRepository<ApplicationUserDb> repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public void CreateUser(ApplicationUser item)
        {
            _repository.Create(_mapper.Map<ApplicationUserDb>(item));
        }

        public bool UserExists(int id)
        {
            if (_repository.Get(id) == null)
            {
                return false;
            }
            return true;
        }

        public ApplicationUser Get(int id)
        {
            return _mapper.Map<ApplicationUser>(_repository.Get(id));
        }

        public IEnumerable<ApplicationUser> GetAllUser()
        {
            return _mapper.Map<IEnumerable<ApplicationUserDb>, IEnumerable<ApplicationUser>>(_repository.GetAll()); ;
        }
    }
}
