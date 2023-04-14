using Microsoft.AspNetCore.Mvc.ModelBinding;
using ModelBinding.Models;

namespace ModelBinding.CustomBinding
{
    public class CustomPersonDetailsBinder : IModelBinder
    {
        private List<PersonDetails> persons = new List<PersonDetails>
        {
            new PersonDetails { Id = 1, Name = "Alex" , Age = 41} ,
            new PersonDetails { Id = 2, Name = "John" , Age = 32} ,
            new PersonDetails { Id = 3, Name = "Jane" , Age = 25} 
        };


        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var modelName = bindingContext.ModelName;
            var value = bindingContext.ValueProvider.GetValue(modelName);
            var result = value.FirstValue;

            if(!int.TryParse(result, out var id))
            {
                return Task.CompletedTask;
            }

            var personDetails = this.persons.Find(f => f.Id == id);

            bindingContext.Result = ModelBindingResult.Success(personDetails);

            return Task.CompletedTask;
        }
    }
}
