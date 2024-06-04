using DUR_Application.Entities;
using FluentValidation;

namespace DUR_Application.Services.Services_Malfunctions.ChangeMalfunctions
{
    public class ChangeMalfunctionsValidator:AbstractValidator<ChangeMalfunctionsDto>
    {
        public ChangeMalfunctionsValidator(DatabaseContext databaseContext)
        {
            RuleFor(pr => pr.Description)
                .MaximumLength(200);


            RuleFor(pr => pr.LayoverTime)
                .LessThan(pr => pr.WorkTime);


            RuleFor(pr => pr.RequestTypeId)
                .Custom((value, context) =>
                {
                    if(value == 0)
                    {
                        return;
                    }

                    var isContains = databaseContext.RequestTypes.First(pr=>pr.Id == value);
                    
                    if(isContains == null)
                    {
                        context.AddFailure($"There is no that request type {value}");
                    }

                });

        }
    }
}
