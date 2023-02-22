
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Models
{
    public class CommentModel
    {
        public string Message { get; set; }
        public bool IsReply { get; set; }
        public int PostId { get; set; }
        public int ParentCommentId { get; set; }
        public int CurrentNested { get; set; }
    }
}
