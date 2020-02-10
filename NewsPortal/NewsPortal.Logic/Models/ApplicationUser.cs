using System.Collections.Generic;

namespace NewsPortal.Logic.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }

        public string GoogleId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public IEnumerable<Post> Posts { get; set; }
    }
}
