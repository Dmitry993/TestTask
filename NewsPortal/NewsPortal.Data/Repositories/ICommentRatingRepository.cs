using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NewsPortal.Data.Models;

namespace NewsPortal.Data.Repositories
{
    public interface ICommentRatingRepository: IRepository<CommentRating>
    {
        Task<CommentRating> FindItem(int commentId, int userId);

        Task DeleteItem(int commentId, int userId);
    }
}
