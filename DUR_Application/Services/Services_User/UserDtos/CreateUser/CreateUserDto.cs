using DUR_Application.Helper;

namespace DUR_Application.Services.Services_User.UserDtos.CreateUser
{
    public class CreateUserDto
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public int RoleID = 6;

        public string UserCode = GenerateRandomCode.GenerateCode();
    }
}
