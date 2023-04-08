using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageLots.Shared
{
    public class LotGetModel
    {
      
        public Currency Currency { get; set; }
        
        public int Amount { get; set; }
       
        public string Seller { get; set; }
        public string MessageId { get; set; }
        public string PopReceipt { get; set; }
    }
}
