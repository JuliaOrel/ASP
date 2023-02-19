using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ModelBinding.Models
{
    public class TrailVM
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(0, 1000)]
        public float TrailLengthKm { get; set; }
        public float TrailLengthM { get; set; }
        public float TrailLength
        {
            get => (float)Math.Round(TrailLengthKm + (float)TrailLengthM / 1000, 3);
            set
            {
                TrailLengthKm = (int)value;
                if (value >= 1)
                {

                    TrailLengthM = (value % (int)value) * 1000;
                }
                else
                {
                    TrailLengthM = value * 1000;
                }
            }
        }
        public string DisplayTrailLength
        {
            get
            {
                return TrailLength == 0 ? "Trail length is not set yet..." : TrailLength.ToString();
            }
        }
            
    }
}
