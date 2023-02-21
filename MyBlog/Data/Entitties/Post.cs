using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Data.Entitties
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Description { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public byte[] MainPostImage { get; set; }
        public bool IsDeleted { get; set; }
        // public string? Tags { get; set; }
        public int? CategoryId { get; set; }
        public string UserId { get; set; } //= default!;
        public Category Category { get; set; }
        public User User { get; set; } //= default!;
        public ICollection<Comment> Comments { get; set; } 
        public ICollection<PostImage> PostImages { get; set; } 
    }
}
