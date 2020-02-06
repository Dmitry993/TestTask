using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPortal.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public int? PostId { get; set; }

        public string UserName { get; set; }

        public string Description { get; set; }

        public DateTime Created { get; set; }
    }
}
