using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProviderConfiguration.MyConfiguration
{
    public class TextConfigurationSource : IConfigurationSource
    {
        public string FilePath { get; set; }
        public TextConfigurationSource(string filePath)
        {
            FilePath = filePath;
        }
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            string path = builder.GetFileProvider().GetFileInfo(FilePath).PhysicalPath;
            return new TextConfigurationProvider(path);
        }
    }
}
