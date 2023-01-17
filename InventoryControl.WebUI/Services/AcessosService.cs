using AWASP.WebUI.Data.Domains;
using AWASP.WebUI.Data.Repositorys.Interfaces;
using AWASP.WebUI.Services.Interfaces;

namespace AWASP.WebUI.Services
{
    public class AcessosService : Service, IAcessosService
    {
        private readonly IRepository<Acesso> _acessosRepository;

        public AcessosService(
            IRepository<Acesso> acessoRepository)
        {
            _acessosRepository = acessoRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Acesso> FindById(int id)
        {
            return await _acessosRepository.FindById(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Acesso> FindByName(string name)
        {
            var acessos = await _acessosRepository.FindAll();
            return acessos.Where(a => a.Nome == name).FirstOrDefault();
        }
    }
}
