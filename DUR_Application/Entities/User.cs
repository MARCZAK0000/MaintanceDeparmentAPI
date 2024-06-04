using Microsoft.AspNetCore.Identity;
using NLog.LayoutRenderers;
using System.Security.Policy;

namespace DUR_Application.Entities
{
    public class User
    {
        public int Id { get; set; } 

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserCode { get; set; }

        public int RoleId { get; set; } 

        public string Password { get; set; }
        
        public string ConfirmPassword { get; set; }

        public Role Role { get; set; }

        public string Email { get; set; }


    }
}
