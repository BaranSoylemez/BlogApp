using BlogApp.Entities;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class CreatePostViewModel
    {
        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public string? Content { get; set; }

        [Required]
        public string? PostUrl { get; set; }
        public string? PostImage { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();  
    }
}
