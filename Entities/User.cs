namespace BlogApp.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserSurname { get; set; }
        public string? Nickname { get; set; }
        public string? UserImage { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
