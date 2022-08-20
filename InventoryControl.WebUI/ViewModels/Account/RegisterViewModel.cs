using InventoryControl.WebUI.ViewModels.Base;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace InventoryControl.WebUI.ViewModels.Account
{
    public class RegisterViewModel : BaseViewModel
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        public bool Enabled { get; set; }
        public List<SelectListItem>? Perfis { get; set; }
        [Required]
        public int? PerfilUsuarioId { get; set; }
        public RegisterViewModel()
        {
            Perfis = new List<SelectListItem>();
        }
    }
}
