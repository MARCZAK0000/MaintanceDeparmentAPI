using System.Security.Claims;

namespace DUR_Application.Services.Services_User.UserContexServices
{

    public interface IUserContextServices
    {
        int? GetUserId { get; }
        ClaimsPrincipal? User { get; }
    }

    public class UserContextServices : IUserContextServices
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserContextServices(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public ClaimsPrincipal? User => _contextAccessor.HttpContext?.User;

        public int? GetUserId => User is null ? null : int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);


    }
}
