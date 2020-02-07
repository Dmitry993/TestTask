namespace NewsPortal.Logic.Models
{
    public class Post
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
