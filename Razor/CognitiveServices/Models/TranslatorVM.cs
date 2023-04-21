using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CognitiveServices.Models
{
    public class TranslatorVM
    {
        public MultiSelectList From { get; set; }
        public MultiSelectList To { get; set; }
        public int FromValue { get; set; }
        public int[] ToValue { get; set; }
        public Languages Languages { get; set; }
        [Required]
        public string Text { get; set; }
        public TranslationResult[] TranslationResults { get; set; }
    }
}
