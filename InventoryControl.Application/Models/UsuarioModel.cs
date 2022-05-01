using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Application.Models
{
    public class UsuarioModel : BaseModel
    {
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