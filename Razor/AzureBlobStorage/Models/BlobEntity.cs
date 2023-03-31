using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureBlobStorage.Models
{
    public class BlobEntity
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Uri { get; set; }
    }
}
