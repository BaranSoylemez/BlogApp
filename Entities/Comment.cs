namespace BlogApp.Entities
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string? CommentContent { get; set; }
        public DateTime? CommentDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int PostId { get; set; }
        public Post Post { get; set; } = null!;
    }
}
