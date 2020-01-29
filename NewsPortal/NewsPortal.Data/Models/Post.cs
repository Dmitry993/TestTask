using NewsPortal.Data.Model;
using System;

namespace NewsPortal.Data.Models
{
    public class Post
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        public User Author { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }        

        public DateTime Created { get; set; }
    }
}
