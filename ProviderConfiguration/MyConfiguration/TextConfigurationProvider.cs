using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProviderConfiguration.MyConfiguration
{
    public class TextConfigurationProvider:ConfigurationProvider
    {
        public string FilePath { get; set; }
        public TextConfigurationProvider(string filePath)
        {
            FilePath = filePath;
        }

        public override void Load()
        {
            var data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            using(StreamReader sr=new StreamReader(FilePath))
            {
                string line;
                while((line=sr.ReadLine())!=null)
                {
                    string key = line.Trim();
                    string value = sr.ReadLine() ?? string.Empty;
                    data.Add(key, value);
                }
            }
            Data = data;
        }
    }
}
