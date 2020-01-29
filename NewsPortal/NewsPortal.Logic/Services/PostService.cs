using AutoMapper;
using NewsPortal.Data.Models;
using NewsPortal.Data.Repositories;
using NewsPortal.Logic.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NewsPortal.Logic.Services
{
    public class PostService : IPostService
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _repository;

        public PostService(IMapper mapper, IPostRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<UserPost> CreatePostAsync(UserPost userPost)
        {
            var post = _mapper.Map<Post>(userPost);
            post.Created = DateTime.UtcNow;
            await _repository.CreateAsync(post);           
            await _repository.SaveAsync();
            return _mapper.Map<UserPost>(post);
        }       

        public async Task<IEnumerable<UserPost>> GetAllPostsAsync()
        {
            var posts = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<Post>, IEnumerable<UserPost>>(posts);
        }

        public async Task<UserPost> GetPostAsync(int id)
        {
            var post = await _repository.GetAsync(id);
            return _mapper.Map<UserPost>(post);
        }
    }
}
