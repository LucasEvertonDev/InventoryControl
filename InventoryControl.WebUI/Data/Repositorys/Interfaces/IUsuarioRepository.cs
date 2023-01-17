using AWASP.WebUI.Data.Domains;

namespace AWASP.WebUI.Data.Repositorys.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<List<Acesso>> FindAcessosByPerfilUsuarioId(int perfilUsuarioId);
    }
}
