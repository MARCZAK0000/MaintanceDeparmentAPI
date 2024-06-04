using FluentValidation;

namespace DUR_Application.Services.Services_Malfunctions.CloseMalfunction
{
    public class CloseMalfunctionValidator:AbstractValidator<CloseMalfunctioDto>
    {
        public CloseMalfunctionValidator()
        {
            RuleFor(pr => pr.Describiton)
               .MaximumLength(200);

            RuleFor(pr => pr.WorkTime)
                .GreaterThanOrEqualTo(pr => pr.LayoverTime)
                .GreaterThan(0);

            RuleFor(pr => pr.LayoverTime)
                .NotEmpty();


        }
    }
}
