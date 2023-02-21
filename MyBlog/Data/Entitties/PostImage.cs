using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Data.Entitties
{
    public class PostImage
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
