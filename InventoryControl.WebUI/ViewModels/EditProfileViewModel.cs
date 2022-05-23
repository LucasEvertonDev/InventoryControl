using InventoryControl.WebUI.Enuns;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace InventoryControl.WebUI.ViewModels
{
    public class EditProfileViewModel : IValidatableObject
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        public bool Enabled { get; set; }

        public List<SelectListItem>? Perfis { get; set; }

        [Required]
        public int? PerfilUsuarioId { get; set; }

        public TipoPessoa TipoPessoas { get; set; }

        [Required]
        public string CpfCnpj { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public DateTime? DataNascimento { get; set; }

        public EditProfileViewModel()
        {
            Perfis = new List<SelectListItem>();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            //if (Nome == "1")
            //{
            //    results.Add(new ValidationResult("The DateEncaissement must be set", new string[] { nameof(Nome)}));
            //}
            return results;
        }
    }
}
