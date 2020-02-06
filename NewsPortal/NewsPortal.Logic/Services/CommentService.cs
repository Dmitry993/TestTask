using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NewsPortal.Data.Models;
using NewsPortal.Data.Repositories;
using NewsPortal.Logic.Model;

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

        public async Task<IEnumerable<UserComment>> GetAllCommentsAsync()
        {
            var comments = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<Comment>, IEnumerable<UserComment>>(comments);
        }

        public async Task<UserComment> GetCommentAsync(int id)
        {
            var comment = await _repository.GetAsync(id);
            return _mapper.Map<UserComment>(comment);
        }

        public async Task<IEnumerable<UserComment>> GetPostCommentsAsync(int id)
        {
            var postComments = new List<UserComment>();
            var comments = await _repository.GetAllAsync();
            var userComments = _mapper.Map<List<UserComment>>(comments);

            foreach (var item in userComments)
            {
                if (item.PostId == id & item.ParentId == null) 
                {
                    postComments.Add(item);
                }

                if(postComments.Any(comment => comment.Id == item.ParentId))
                {
                    postComments.Where(comment => comment.Id == item.ParentId)
                        .ToList()
                        .ForEach(comment => comment.Reply.Add(item));
                }
            }

            return postComments;
        }

        public async Task<UserComment> CreateCommentAsync(UserComment userComment)
        {
            var comment = _mapper.Map<Comment>(userComment);
            comment.Created = DateTime.UtcNow;
            await _repository.CreateAsync(comment);
            await _repository.SaveAsync();
            return _mapper.Map<UserComment>(comment);
        }
    }
}
