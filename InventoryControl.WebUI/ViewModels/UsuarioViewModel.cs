using System.ComponentModel.DataAnnotations;

namespace AWASP.WebUI.ViewModels
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }

        [Required]
        public string? Login { get; set; }
        [Required]
        public string? Senha { get; set; }
        public string? CpfCnpj { get; set; }
        [Required]
        public string? Nome { get; set; }
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public int? PerfilUsuarioId { get; set; }
        [Required]
        public int? Situacao { get; set; }
    }
}