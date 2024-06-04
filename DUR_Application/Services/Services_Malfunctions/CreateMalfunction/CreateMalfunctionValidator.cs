using DUR_Application.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Identity.Client;

namespace DUR_Application.Services.Services_Malfunctions.CreateMalfunction
{
    public class CreateMalfunctionValidator : AbstractValidator<CreateMalfunctionDto>
    {
        public CreateMalfunctionValidator(DatabaseContext databaseContext )
        {
            RuleFor(pr=>pr.Name)
                .NotNull() 
                .MaximumLength(100)
                .MinimumLength(5);

            RuleFor(pr => pr.CreatedTime)
                .NotNull()
                .NotEmpty()
                .Custom((value, context) =>
                {
                    if(value > DateTime.Now)
                    {
                        context.AddFailure("CreatedTime", "Wrong date");
                    }
                });

            RuleFor(pr => pr.RequestTypeId)
                .Custom((value, context) =>
                {
                    var find = databaseContext.RequestTypes.Any(pr => pr.Id == value);
                    if(!find) 
                    {
                        context.AddFailure("Request Type Id", "Wrong request type Id");
                    }
                });
        }
    }
}
