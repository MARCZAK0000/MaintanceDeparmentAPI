using DUR_Application.Entities;
using FluentValidation;

namespace DUR_Application.Services.Services_User.UserDtos.UpdateRole
{
    public class UpdateRoleValidtor:AbstractValidator<UpdateRoleDto>
    {
        public UpdateRoleValidtor(DatabaseContext databaseContext)
        {
            RuleFor(pr => pr.Id)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var isContains = databaseContext.Users.Any(pr => pr.Id == value);
                    if (!isContains) 
                    {
                        context.AddFailure($"{nameof(value)}", "Wrong Id");
                    }
                });


            RuleFor(pr => pr.RoleID)
                .NotEmpty()
                .Custom((value, context) =>
                {
                    var isContains = databaseContext.Roles.Any(pr => pr.Id == value);
                    if (!isContains) 
                    {
                        context.AddFailure($"{nameof(value)}", "Wrong Role Id");
                    }
                });
        }
    }
}
