using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPortal.Logic.Model
{
    public class UserPost
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
