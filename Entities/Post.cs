namespace BlogApp.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? PostUrl { get; set; }
        public string? PostImage { get; set; }
        public DateTime PostDate { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public List<Tag> Tags { get; set; }= new List<Tag>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
