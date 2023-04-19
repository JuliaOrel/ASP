using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CognitiveServices.Models
{
    public class GetImageModel
    {
        public byte[] ImageData { get; set; }
        public string ComputerVisionStatus { get; set; }
    }
}
