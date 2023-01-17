using AWASP.WebUI.Data.Domains;

namespace AWASP.WebUI.Services.Interfaces
{
    public interface IAcessosService
    {
        Task<Acesso> FindById(int id);
        Task<Acesso> FindByName(string name);
    }
}
