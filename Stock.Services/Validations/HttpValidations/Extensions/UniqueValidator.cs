using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentValidation.Resources;
using FluentValidation.Validators;
using Stock.Data;

namespace Stock.Services.HttpValidations.Extensions
{
    public class UniqueValidator<T> : PropertyValidator
        where T: class
    {

        private readonly IEnumerable<T> _items;

        public UniqueValidator(IEnumerable<T> items)
            : base("{PropertyName} must be unique")
        {
            _items = items;
        }


        protected override bool IsValid(PropertyValidatorContext context)
        {
           var editedItem = context.InstanceToValidate as T;
           var newValue = context.PropertyValue as string;
           var property = typeof(T).GetTypeInfo().GetDeclaredProperty(context.PropertyName);
           return _items.All(item =>
               item.Equals(editedItem) || property.GetValue(item).ToString() != newValue);
        }
    }
}