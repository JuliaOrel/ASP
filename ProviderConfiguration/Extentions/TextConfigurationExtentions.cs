using Microsoft.Extensions.Configuration;
using ProviderConfiguration.MyConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProviderConfiguration.Extentions
{
    public static class TextConfigurationExtentions
    {
        public static IConfigurationBuilder AddMyTextFileConfig ( this IConfigurationBuilder builder, string path)
        {
            if(builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if(string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("File path doesn't exist");
            }
            TextConfigurationSource textConfigurationSource =
                new TextConfigurationSource(path);
            builder.Add(textConfigurationSource);
            return builder;
        }
    }
}
