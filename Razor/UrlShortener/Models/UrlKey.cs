using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Text;

namespace UrlShortener.Models
{
    public class UrlKey: ITableEntity
    {
        public int Id { get; set; }
        public string PartitionKey { get;set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
