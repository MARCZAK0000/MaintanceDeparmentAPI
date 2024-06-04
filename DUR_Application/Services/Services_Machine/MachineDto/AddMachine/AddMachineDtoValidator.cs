using FluentValidation;

namespace DUR_Application.Services.Services_Machine.MachineDto.AddMachine
{
    public class AddMachineDtoValidator : AbstractValidator<AddMachineDto>
    {
        public AddMachineDtoValidator()
        {
            RuleFor(pr=>pr.LaneNumber)
                .Length(8)
                .NotEmpty();
                
            RuleFor(pr=>pr.MachineName)
                .MaximumLength(100)
                .NotEmpty();

            RuleFor(pr=>pr.MachineDescription)
                .MaximumLength(150)
                .NotEmpty();


        }
    }
}
