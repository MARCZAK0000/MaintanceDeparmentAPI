using DUR_Application.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace DUR_Application.Services.Services_Magazine.AddSpareParts
{
    public class AddSparePartsValidator : AbstractValidator<AddSparePartsDto>
    {
        public AddSparePartsValidator(DatabaseContext databaseContext)
        {
            RuleFor(pr => pr.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(8)
                .MaximumLength(100)
                .Custom((value, context) =>
                {
                    var isContains = databaseContext.SpareParts.Any(pr=>pr.Name == value);
                    if(isContains)
                    {
                        context.AddFailure("Name", $"There is a part with the same name like {value} in Database");
                    }
                });


            RuleFor(pr => pr.Type)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(25); 

            RuleFor(pr => pr.Description)
                .NotEmpty()
                .NotNull()
                .MinimumLength(8)
                .MaximumLength(100);

            RuleFor(pr => pr.Price)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0); 
            
            RuleFor(pr => pr.Count)
                .NotEmpty()
                .NotNull()
                .GreaterThanOrEqualTo(0);
        }
    }
}
