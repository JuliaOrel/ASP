using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.Models
{
    public class Comment
    {
        public int ID { get; set; }
        [Display(Name ="Write your comment")]
        public string TextComment { get; set; }
        public int NewsOneId { get; set; }
        public NewsOne NewsOne { get; set; }
    }
}
