using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Medicines.Authorization
{
    public class MyPolicies
    {
        public const string SuperAdminAccessOnly = "SuperAdminAccessOnly";
        public const string AdminAndAboveAccess = "AdminAndAboveAccess";
        public const string PostsWriterAndAboveAccess = "PostsWriterAndAboveAccess";

    }
}
