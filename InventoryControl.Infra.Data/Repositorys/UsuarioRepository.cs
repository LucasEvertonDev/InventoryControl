using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces;
using InventoryControl.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Infra.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Acesso>> FindAcessosByPerfilUsuarioId(int perfilUsuarioId)
        {
            return await _applicationDbContext.MapPerfilUsuariosAcessos
                .Include(c => c.Acesso)
                .Where(p => p.PerfilUsuarioId == perfilUsuarioId)
                .Select(a => a.Acesso)
                .ToListAsync();
        }
    }
}
