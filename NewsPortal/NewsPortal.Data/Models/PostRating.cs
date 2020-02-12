using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPortal.Data.Models
{
    public class PostRating
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public bool Value { get; set; }
        public DateTime Created { get; set; }
    }
}
