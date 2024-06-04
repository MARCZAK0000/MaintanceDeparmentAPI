using DUR_Application.Entities;
using DUR_Application.Model.Response;
using DUR_Application.Services.Services_User.UserDtos.CreateUser;
using DUR_Application.Services.Services_User.UserDtos.LoginUser;
using DUR_Application.Services.Services_User.UserDtos.ShowUser;

namespace DUR_Application.Services.Services_User.UserServicesController
{
    public interface IUserServices
    {
        Task CreateUser(CreateUserDto create);
        
        Task <string> LoginUser(LoginUserDto loginUser);

        string GenerateJwtToken(User login);

        Task<Response<ShowUserDto>> UpdateRole();

    }
}
