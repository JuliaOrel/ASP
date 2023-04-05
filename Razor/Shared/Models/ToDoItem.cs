using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsShared.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string Tip { get; set; }
        public int TimeToComplete { get; set; }
        public bool IsCompleted { get; set; }
        public string SecretTip { get; set; }
    }
}
