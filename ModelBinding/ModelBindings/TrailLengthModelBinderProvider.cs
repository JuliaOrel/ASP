using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelBinding.ModelBindings
{
    public class TrailLengthModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            ILoggerFactory loggerFactory = context.Services.GetService<ILoggerFactory>();
            IModelBinder modelBinder = new TrailLengthModelBinder(
            new SimpleTypeModelBinder(typeof(float), loggerFactory));
            return context.Metadata.ModelType ==typeof(float) ? modelBinder:null;
        }
    }
}
