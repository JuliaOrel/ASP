using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsShared.Models
{
    public class ToDoItemDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Action can't be null")]
        [MinLength(2)]
        public string Action { get; set; }
        public string Tip { get; set; }
        public int HoursToComplete { get; set; }
        public int MinutesToComplete { get; set; }
        public bool IsCompleted { get; set; }
        //public string SecretTip { get; set; }
        public int TimeToComplete {
            get => HoursToComplete * 60 + MinutesToComplete;
            set
            {
                HoursToComplete = value / 60;
                MinutesToComplete = value % 60;
            }
        }
    }
}
