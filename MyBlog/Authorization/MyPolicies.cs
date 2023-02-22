using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Authorization
{
    public static class MyPolicies
    {
        public const string SuperAdminAccessOnly = "SuperAdminAccessOnly";
        public const string AdminAndAboveAccess = "AdminAndAboveAccess";
        public const string PostsWriterAndAboveAccess = "PostsWriterAndAboveAccess";
    }
}
