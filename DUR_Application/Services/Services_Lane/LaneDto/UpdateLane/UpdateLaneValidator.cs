using DUR_Application.Entities;
using FluentValidation;

namespace DUR_Application.Services.Services_Lane.LaneDto.UpdateLane
{
    public class UpdateLaneValidator:AbstractValidator<UpdateLaneDto>
    {
        public UpdateLaneValidator(DatabaseContext database)
        {

            RuleFor(pr => pr.Describtion)
                .NotEmpty()
                .MaximumLength(100);
        }
    }
}
