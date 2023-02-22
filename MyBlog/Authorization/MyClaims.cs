using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Authorization
{
    public static class MyClaims
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string Admin = "Admin";
        public const string PostsWriter = "PostsWriter";
    }
}
