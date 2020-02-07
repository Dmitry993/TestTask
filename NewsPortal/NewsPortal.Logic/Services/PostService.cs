using AutoMapper;
using NewsPortal.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NewsPortal.Logic.Models;

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
        public async Task<Post> CreatePostAsync(Post userPost)
        {
            var post = _mapper.Map<Data.Models.Post>(userPost);
            post.Created = DateTime.UtcNow;
            await _repository.CreateAsync(post);           
            await _repository.SaveAsync();
            return _mapper.Map<Post>(post);
        }

        public async Task<IEnumerable<Post>> GetUserPostsAsync(int id)
        {
            var post = await _repository.FindPostsByUserId(id);
            return _mapper.Map<IEnumerable<Post>>(post);
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            var posts = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<Data.Models.Post>, IEnumerable<Post>>(posts);
        }

        public async Task<Post> GetPostAsync(int id)
        {
            var post = await _repository.GetAsync(id);
            return _mapper.Map<Post>(post);
        }

        public async Task<Post> UpdatePostAsync(Post userPost)
        {
            var post = _mapper.Map<Data.Models.Post>(userPost);
            _repository.Update(post);
            await _repository.SaveAsync();
            return _mapper.Map<Post>(post);
        }
    }
}
