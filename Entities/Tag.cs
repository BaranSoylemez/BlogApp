﻿namespace BlogApp.Entities
{
    public enum TagColor
    {
        primary, danger, warning, secondary,success
    }
    public class Tag
    {
        public int TagId { get; set; }
        public string? Text { get; set; }
        public string? TagUrl { get; set; }
        public TagColor? Color { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();
    }
}
