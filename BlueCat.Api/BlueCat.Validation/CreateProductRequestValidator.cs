using BlueCat.Contract;
using FluentValidation;

namespace BlueCat.Validation
{
    public class GetStudentsRequestValidator : AbstractValidator<GetStudentsRequest>
    {
        public GetStudentsRequestValidator()
        {
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
        }
    }
}
