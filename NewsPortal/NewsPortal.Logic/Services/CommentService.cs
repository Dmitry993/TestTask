using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NewsPortal.Data.Repositories;
using NewsPortal.Logic.Models;

namespace NewsPortal.Logic.Services
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepository _repository;

        public CommentService(ICommentRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsAsync()
        {
            var comments = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<Data.Models.Comment>, IEnumerable<Comment>>(comments);
        }

        public async Task<Comment> GetCommentAsync(int id)
        {
            var comment = await _repository.GetAsync(id);
            return _mapper.Map<Comment>(comment);
        }

        public async Task<IEnumerable<Comment>> GetPostCommentsAsync(int postId)
        {
            var postComments = new List<Comment>();
            var comments = await _repository.GetCommentsByPostId(postId);
            var mappedComments = _mapper.Map<List<Comment>>(comments);

            foreach (var item in mappedComments)
            {
                if (item.PostId == postId && item.ParentId == null)
                {
                    postComments.Add(item);
                }

                postComments.Where(comment => comment.Id == item.ParentId)
                        .ToList()
                        .ForEach(comment => comment.Replies.Add(item));
            }

            return postComments;
        }

        public async Task<Comment> CreateCommentAsync(Comment сomment)
        {
            var mappedComment = _mapper.Map<Data.Models.Comment>(сomment);
            mappedComment.Created = DateTime.UtcNow;
            await _repository.CreateAsync(mappedComment);
            await _repository.SaveAsync();
            return _mapper.Map<Comment>(mappedComment);
        }
    }
}
