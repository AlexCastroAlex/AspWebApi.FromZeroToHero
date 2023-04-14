using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ModelBinding.CustomBinding
{
    public class CustomBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var data = bindingContext.HttpContext.Request.Query;
            var result = data.TryGetValue("nationalities", out var nationalities);

            if(result)
            {
                var array = nationalities.ToString().Split('|');
                bindingContext.Result = ModelBindingResult.Success(array);
            }

            return Task.CompletedTask;
        }
    }
}
