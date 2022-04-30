using InventoryControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace InventoryControl.WebUI.Identity
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
