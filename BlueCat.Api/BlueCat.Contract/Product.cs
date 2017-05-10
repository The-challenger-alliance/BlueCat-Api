using BlueCat.Api.Common;
using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueCat.Contract
{
    //[Validator(typeof(ProductValidator))]
    public class Product:Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }

    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {

            //RuleFor(x => x.Name).Must((x, Name) =>
            //{
            //    if (string.IsNullOrEmpty(Name))
            //    {
            //        return false;
            //    }

            //    return Name.Length <= 50 ? true : false;

            //}).WithName("Name").WithMessage("a mandatory field of type<ControllerName>");
            RuleFor(x => x.Name).Must((x, Name) =>
            {
                if (Name.Length > 2)
                {
                    return true;
                }
                else 
                {
                    return false;
                }
            }).WithMessage("a mandatory field of type<Name>");

            //RuleFor(m => m.Name).NotEmpty().WithMessage("Validation failed");

        }
    }
}
