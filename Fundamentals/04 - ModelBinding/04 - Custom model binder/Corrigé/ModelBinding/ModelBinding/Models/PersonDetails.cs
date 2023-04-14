using Microsoft.AspNetCore.Mvc;
using ModelBinding.CustomBinding;

namespace ModelBinding.Models
{
    [ModelBinder(BinderType = typeof(CustomPersonDetailsBinder))]
    public class PersonDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
