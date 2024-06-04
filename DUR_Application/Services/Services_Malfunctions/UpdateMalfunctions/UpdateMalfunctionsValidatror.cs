using DUR_Application.Entities;
using FluentValidation;

namespace DUR_Application.Services.Services_Malfunctions.UpdateMalfunctions
{
    public class UpdateMalfunctionsValidatror :AbstractValidator<UpdateMalfunctionsDto>
    {
        public UpdateMalfunctionsValidatror(DatabaseContext databaseContext)
        {
            RuleFor(pr => pr.Describiton)
                .NotEmpty()
                .MaximumLength(255);

            RuleFor(pr => pr.UserNumber).
                NotEmpty()
                .Length(11);


            RuleFor(pr => pr.RequestTypeId)
                .Custom((value, context) =>
                {
                    if (value == 0)
                    {
                        return;
                    }

                    var isContains = databaseContext.RequestTypes.Any(pr => pr.Id == value);

                    if (!isContains)
                    {
                        context.AddFailure($"There is no that request type {value}");
                    }

                });

        }
    }
}
