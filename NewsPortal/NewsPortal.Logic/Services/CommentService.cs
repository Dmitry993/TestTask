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
            var comments = await _repository.GetCommentsByPostId(postId);
            var mappedComments = _mapper.Map<List<Comment>>(comments);
            var topComments = mappedComments.Where(comment => comment.ParentId == null).ToList();
            topComments.ForEach(comment =>  AddReplies(comment, mappedComments));

            return topComments;
        }

        public async Task<Comment> CreateCommentAsync(Comment сomment)
        {
            var mappedComment = _mapper.Map<Data.Models.Comment>(сomment);
            mappedComment.Created = DateTime.UtcNow;
            await _repository.CreateAsync(mappedComment);
            await _repository.SaveAsync();
            return _mapper.Map<Comment>(mappedComment);
        }

        private void AddReplies(Comment comment, List<Comment> postComments)
        {
            comment.Replies = postComments.Where(reply => reply.ParentId == comment.Id).ToList();
            comment.Replies.ForEach(childComment => AddReplies(childComment, postComments));
        }

        public async Task IncreaseRatingAsync(int commentId)
        {
            var comment = await _repository.GetAsync(commentId);
            comment.Rating++;
            await _repository.UpdateAndSaveAsync(comment);
        }

        public async Task DecreaseRatingAsync(int commentId)
        {
            var comment = await _repository.GetAsync(commentId);
            comment.Rating--;
            await _repository.UpdateAndSaveAsync(comment);
        }
    }
}
