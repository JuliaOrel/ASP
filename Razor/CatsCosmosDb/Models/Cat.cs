using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatsCosmosDb.Models
{
    public class Cat
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        [Required]
        public string Name { get; set; }
        [JsonProperty("breed")]
        [Required]
        public string Breed { get; set; }

    }
}
