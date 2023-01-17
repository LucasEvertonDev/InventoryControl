using AWASP.WebUI.Data.Domains;
using AWASP.WebUI.ViewModels;

namespace AWASP.WebUI.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<IList<PerfilUsuario>> FindPerfisUsuario();
        Task<Usuario> FindById(int id);
        Task<Usuario> FindByName(string name);
        Task<Usuario> FindByUsername(string userName);
        Task<List<Acesso>> FindAcessosByPerfilUsuarioId(int perfilUsuario);
        Task<Usuario> Login(string userName, string password);
        Task<Usuario> CreateUsuario(UsuarioViewModel model);
    }
}
