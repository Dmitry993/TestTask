using NewsPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPortal.Data.Model
{
    public class User   
    {
        public int Id { get; set; }

        public string GoogleId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public IEnumerable<Post> Posts { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}
