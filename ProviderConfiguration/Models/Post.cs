using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProviderConfiguration.Models
{
    public class Post
    {
        public string PostTitle { get; set; }
        public string PostDescription { get; set; }
        public Author Author { get; set; }
    }
}
