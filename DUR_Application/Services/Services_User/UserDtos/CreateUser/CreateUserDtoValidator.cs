using DUR_Application.Entities;
using FluentValidation;

namespace DUR_Application.Services.Services_User.UserDtos.CreateUser
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
      
        public CreateUserDtoValidator(DatabaseContext databaseContext)
        {

            RuleFor((pr => pr.Email)).NotEmpty().NotEmpty().EmailAddress().MaximumLength(100).Custom((value, context) =>
            {
                var findUser = databaseContext.Users.Any(prop => prop.Email == value);
                if (findUser)
                {
                    context.AddFailure("Email is in used");
                }

            });

            RuleFor((pr => pr.FirstName))
                .NotEmpty()
                .MaximumLength(50)
                .MinimumLength(3);

            RuleFor((pr => pr.LastName))
                .NotEmpty()
                .MaximumLength(50)
                .MinimumLength(3);

            RuleFor((pr => pr.Password))
                .NotEmpty()
                .MaximumLength(18)
                .MinimumLength(6);

            RuleFor((pr => pr.ConfirmPassword))
                .NotEmpty()
                .MaximumLength(50)
                .MinimumLength(3);

            RuleFor((pr => pr.UserCode))
                .NotEmpty()
                .MaximumLength(50)
                .MinimumLength(3);

        }
    }
}
