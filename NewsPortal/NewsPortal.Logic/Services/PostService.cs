﻿using AutoMapper;
using NewsPortal.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsPortal.Logic.Enums;
using NewsPortal.Logic.Models;


namespace NewsPortal.Logic.Services
{
    public class PostService : IPostService
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _repository;
        private readonly ICommentService _service;

        public PostService(IMapper mapper, IPostRepository repository,
            ICommentService service)
        {
            _repository = repository;
            _service = service;
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
            var comments = await _service.GetPostCommentsAsync(id);
            var mappedPost = _mapper.Map<Post>(post);
            mappedPost.Comments = comments.ToList();
            return mappedPost;
        }

        public async Task IncreaseRatingAsync(int postId)
        {
            var post = await _repository.GetAsync(postId);
            post.Rating++;
            await _repository.UpdateAndSaveAsync(post);
        }

        public async Task DecreaseRatingAsync(int postId)
        {
            var post = await _repository.GetAsync(postId);
            post.Rating--;
            await _repository.UpdateAndSaveAsync(post);
        }

        public async Task<Post> UpdatePostAsync(Post userPost)
        {
            var post = _mapper.Map<Data.Models.Post>(userPost);
            userPost.Created = DateTime.UtcNow;
            _repository.Update(post);
            await _repository.SaveAsync();
            return _mapper.Map<Post>(post);
        }

        public IEnumerable<Post> GetSortedPosts(SortBy sort, bool isDescending, int userId)
        {
            var sortedPosts = sort == SortBy.Date 
                ? _repository.GetSortedPosts(userId, post => post.Created, isDescending)
                : _repository.GetSortedPosts(userId, post => post.Rating, isDescending);

            return _mapper.Map<IEnumerable<Post>>(sortedPosts);
        }
    }
}
