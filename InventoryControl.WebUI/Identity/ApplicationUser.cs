using Microsoft.AspNetCore.Identity;

namespace AWASP.WebUI.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public ApplicationUser()
        { 
            Roles = new List<string>();
            PasswordHash = "";
        }
        public IList<string> Roles { get; set; } 
    }
}
