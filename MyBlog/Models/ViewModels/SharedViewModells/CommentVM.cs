using MyBlog.Data.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Models.ViewModels.SharedViewModells
{
    public class CommentVM
    {
        public Comment Comment { get; set; }
        public bool IsReply { get; set; }
        public int CurrentNested { get; set; }
        public const int MaxNestes = 5; 
        public string BackgroundColor
        {
            get => CurrentNested % 2 == 0 ? "lightgray" : "white";
        }
    }
}
