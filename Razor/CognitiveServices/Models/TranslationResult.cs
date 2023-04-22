using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CognitiveServices.Models
{
    public class DetectedLanquage
    {
        public string Language { get; set; }
        public double Score { get; set; }
    }

    public class Translation
    {
        public string Text { get; set; }
        public string To { get; set; }
    }

    public class TranslationResult
    {
        public DetectedLanquage DetectedLanquage { get; set; }
        public IList<Translation> Translations { get; set; }
    }
}
