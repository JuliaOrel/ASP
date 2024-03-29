﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLExample
{
        public class Datum
        {

            [JsonProperty("instant")]
            public int Instant { get; set; }

            [JsonProperty("date")]
            public DateTime Date { get; set; }

            [JsonProperty("season")]
            public int Season { get; set; }

            [JsonProperty("yr")]
            public int Yr { get; set; }

            [JsonProperty("mnth")]
            public int Mnth { get; set; }

            [JsonProperty("weekday")]
            public int Weekday { get; set; }

            [JsonProperty("weathersit")]
            public int Weathersit { get; set; }

            [JsonProperty("temp")]
            public double Temp { get; set; }

            [JsonProperty("atemp")]
            public double Atemp { get; set; }

            [JsonProperty("hum")]
            public double Hum { get; set; }

            [JsonProperty("windspeed")]
            public double Windspeed { get; set; }
        }

        public class Inputs
        {

            [JsonProperty("data")]
            public IList<Datum> Data { get; set; }
        }

        public class GlobalParameters
        {

            [JsonProperty("quantiles")]
            public IList<double> Quantiles { get; set; }
        }

        public class RequestData
        {

            [JsonProperty("Inputs")]
            public Inputs Inputs { get; set; }

            [JsonProperty("GlobalParameters")]
            public GlobalParameters GlobalParameters { get; set; }
        }


    }

