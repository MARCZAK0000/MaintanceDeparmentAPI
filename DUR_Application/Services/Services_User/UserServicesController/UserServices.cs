using DUR_Application.Entities;
using DUR_Application.Exceptions;
using DUR_Application.Model.Response;
using DUR_Application.Services.Services_User.UserDtos.CreateUser;
using DUR_Application.Services.Services_User.UserDtos.LoginUser;
using DUR_Application.Services.Services_User.UserDtos.ShowUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DUR_Application.Services.Services_User.UserServicesController
{
    public class UserServices : IUserServices
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ILogger<UserServices> _logger;
        private readonly AuthenticationSettings _authenticationSettings;
        
        public UserServices(DatabaseContext databaseContext, IPasswordHasher<User> passwordHasher, 
            ILogger<UserServices> logger, AuthenticationSettings authenticationSettings)
        {
            _databaseContext = databaseContext;
            _passwordHasher = passwordHasher;
            _logger = logger;
            _authenticationSettings = authenticationSettings;
        }

        public async Task CreateUser(CreateUserDto create)
        {
            if(create == null)
            {
                throw new NotFoundException("Empty request - create user");
            }
            var newUser = new User()
            {
                Email = create.Email,
                FirstName = create.FirstName,
                LastName = create.LastName,
                RoleId = create.RoleID,
                UserCode = create.UserCode,
            };
            var checkPassword = create.Password.ToLower().Equals(create.ConfirmPassword.ToLower()) ? true : false;
            if (!checkPassword)
            {
                throw new WrongDataException("Password is wrong");
            }
            var hashPassword = _passwordHasher.HashPassword(newUser, create.Password);
            newUser.Password = hashPassword;
            newUser.ConfirmPassword = hashPassword;

            await _databaseContext.Users.AddAsync(newUser);
            await _databaseContext.SaveChangesAsync();
            _logger.LogInformation($"Create user: {newUser.Email}");
            
        }

        public string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim("Number", user.UserCode)

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credenitals = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expireDays = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer, claims, null, expireDays, credenitals);

            return new JwtSecurityTokenHandler().WriteToken(token); 
        }

        public async Task<string> LoginUser(LoginUserDto loginUser)
        {
            var user = await _databaseContext.Users
                .Include(pr=>pr.Role)
                .FirstAsync(u => u.Email == loginUser.Email);

            if(user is null)
            {
                throw new BadRequestException("Invalid email or password");
            }

            var result =  _passwordHasher.VerifyHashedPassword(user, user.Password, loginUser.Password);

            if(result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid Email or password");
            }

            var token = GenerateJwtToken(user);


            return token;

        }

        public Task<Response<ShowUserDto>> UpdateRole()
        {
            throw new NotImplementedException();
        }
    }
}
