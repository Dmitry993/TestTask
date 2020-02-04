using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPortal.Logic.Model
{
    public class UserComment
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public int? PostId { get; set; }

        public string UserName { get; set; }

        public string Description { get; set; }

        public List<UserComment> Reply { get; set; } = new List<UserComment>();
    }
}
