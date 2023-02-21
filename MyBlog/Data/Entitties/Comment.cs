
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Data.Entitties
{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; } 
        public DateTime Created { get; set; }
        public int PostId { get; set; }
        public string UserId { get; set; } 
        public Post Post { get; set; } //= default!;
        public User User { get; set; } //= default!;
        public int? ParentCommentId { get; set; }
        [ForeignKey(nameof(ParentCommentId))]
        public Comment ParentComment { get; set; }
        public ICollection<Comment> ChildComments { get; set; }
    }
}
