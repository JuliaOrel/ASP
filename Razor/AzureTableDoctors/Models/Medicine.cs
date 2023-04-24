using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AzureTableDoctors.Models
{
    public class Medicine : ITableEntity
    {
        public string Id { get; set; }
        [Required]
        public string MedicineName { get; set; }
        [Required]
        public string Category { get; set; }
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        public string Instruction { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
