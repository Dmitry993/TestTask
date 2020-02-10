using System.Collections.Generic;

namespace NewsPortal.Logic.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int? ParentId { get; set; } = null;

        public int PostId { get; set; }

        public int UserId { get; set; }

        public ApplicationUser Author { get; set; }

        public string Description { get; set; }

        public List<Comment> Replies { get; set; } = new List<Comment>();
    }
}
