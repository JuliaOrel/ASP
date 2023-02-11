using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProviderConfiguration.Models
{
    public class Blog
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<Post>Posts { get; set; }
    }

}
