using InventoryControl.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario> FindById(int id);
        Task<Usuario> FindByName(string name);
        Task<Usuario> FindByUsername(string userName);
        Task<List<Acesso>> FindAcessosByPerfilUsuarioId(int perfilUsuario);
    }
}
