using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelBinding.ModelBindings
{
    public class TrailLengthModelBinder : IModelBinder
    {
        private readonly IModelBinder _originalModelBinder;
        public TrailLengthModelBinder(IModelBinder originalModelBinder)
        {
            _originalModelBinder = originalModelBinder;
        }
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext is null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }
            var kilometersPartValue = bindingContext.ValueProvider.GetValue("kilometers");
            var metersPartValue = bindingContext.ValueProvider.GetValue("meters");
            if (kilometersPartValue == ValueProviderResult.None || metersPartValue == ValueProviderResult.None)
            {
                return _originalModelBinder.BindModelAsync(bindingContext);
            }
            string kilometers = kilometersPartValue.FirstValue;
            string meters = metersPartValue.FirstValue;

            int.TryParse(kilometers, out int kilometersParsed);
            int.TryParse(meters, out int metersParsed);

            float result = kilometersParsed + (float)metersParsed / 1000;
            // встановлюємо результат привязки
            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }
    }
}
