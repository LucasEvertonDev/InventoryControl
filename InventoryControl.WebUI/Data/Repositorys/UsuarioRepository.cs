using AWASP.WebUI.Data.Contexts;
using AWASP.WebUI.Data.Domains;
using AWASP.WebUI.Data.Repositorys.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AWASP.WebUI.Data.Repositorys
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
