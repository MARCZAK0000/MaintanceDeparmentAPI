using DUR_Application.Entities;
using FluentValidation;

namespace DUR_Application.Services.Services_Lane.LaneDto.CreateLane
{
    public class CreateLaneValidator : AbstractValidator<CreateLaneDto>
    {
        public CreateLaneValidator(DatabaseContext databaseContext)
        {
            RuleFor(pr => pr.Number)
                .NotEmpty()
                .Length(8)
                .Custom((value, context) =>
                {
                    var checkLane = databaseContext.Lanes.Any(pr=>pr.Number == value);
                    if (checkLane)
                    {
                        context.AddFailure("There is lane with that number");
                    }
                });

            RuleFor(pr => pr.Describtion)
                .NotEmpty()
                .MaximumLength(200);

        }
    }
}
