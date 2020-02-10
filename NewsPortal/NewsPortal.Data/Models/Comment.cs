using System;
using System.Collections.Generic;
using System.Text;
using NewsPortal.Data.Model;

namespace NewsPortal.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public int PostId { get; set; }

        public int UserId { get; set; }

        public User Author { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }
    }
}
