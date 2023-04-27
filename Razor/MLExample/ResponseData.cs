using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLExample
{
    public class Index
    {

        [JsonProperty("date")]
        public long Date { get; set; }
    }

    public class Results
    {

        [JsonProperty("forecast")]
        public IList<double> Forecast { get; set; }

        [JsonProperty("prediction_interval")]
        public IList<string> PredictionInterval { get; set; }

        [JsonProperty("index")]
        public IList<Index> Index { get; set; }
    }

    public class ResponseData
    {

        [JsonProperty("Results")]
        public Results Results { get; set; }
    }


}
