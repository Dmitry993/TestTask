using NewsPortal.Data.Model;
using System;
using System.Collections.Generic;

namespace NewsPortal.Data.Models
{
    public class Post
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        public int RatingId { get; set; }

        public User Author { get; set; }

        public int Rating { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }
    }
}
