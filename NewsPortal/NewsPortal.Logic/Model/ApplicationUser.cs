using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPortal.Logic.Model
{
    public class ApplicationUser
    {
        public int Id { get; set; }

        public string GoogleId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public IEnumerable<UserPost> Posts { get; set; }
    }
}
