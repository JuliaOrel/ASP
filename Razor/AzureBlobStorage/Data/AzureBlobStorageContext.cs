using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AzureBlobStorage.Models;

namespace AzureBlobStorage.Data
{
    public class AzureBlobStorageContext : DbContext
    {
        public AzureBlobStorageContext (DbContextOptions<AzureBlobStorageContext> options)
            : base(options)
        {

        }

        public DbSet<AzureBlobStorage.Models.BlobEntity> BlobEntities { get; set; }
    }
}
