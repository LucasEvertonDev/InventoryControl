using AutoMapper;
using InventoryControl.Application.Interfaces;
using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces;
using InventoryControl.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryControl.Application.Services
{
    public class UsuarioService : Service, IUsuarioService
    {
        private readonly IRepository<Usuario> _usuarioRepository;
        private readonly IRepository<MapPerfilUsuariosAcessos> _mapPerfilUsuariosAcessosRepository;
        private readonly IRepository<PerfilUsuario> _perfilUsuarioRepository;

        public IMapper _imapper { get; }

        public UsuarioService(
            IRepository<Usuario> usuarioRepository,
            IRepository<MapPerfilUsuariosAcessos> mapPerfilUsuariosAcessos,
            IRepository<PerfilUsuario> perfilUsuarioRepository,
            IMapper imapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapPerfilUsuariosAcessosRepository = mapPerfilUsuariosAcessos;
            _perfilUsuarioRepository = perfilUsuarioRepository;
            _imapper = imapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Usuario> FindById(int id)
        {
            return await _usuarioRepository.FindById(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Usuario> FindByName(string name)
        {
            return  await _usuarioRepository.Table.Where(a => a.Nome == name).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="perfilUsuario"></param>
        /// <returns></returns>
        public async Task<List<Acesso>> FindAcessosByPerfilUsuarioId(int perfilUsuario)
        { 
            return await _mapPerfilUsuariosAcessosRepository.Table.Include(c => c.Acesso)
                .Where(p => p.PerfilUsuarioId == perfilUsuario)
                .Select(a => a.Acesso)
                .ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<Usuario> FindByUsername(string userName)
        {
            return  await _usuarioRepository.Table.Where(a => a.Login.ToLower() == userName.ToLower()).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Usuario> Login(string userName, string password)
        {
            var user = await this.FindByUsername(userName);
            if (user == null || user.Id < 1)
            {
                LogicalException("Username not registered!");
            }

            if (!user.Senha.Equals(password))
            {
                LogicalException("Invalid password!");
            }
            return user;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IList<PerfilUsuario>> FindPerfisUsuario()
        {
            return await _perfilUsuarioRepository.Table.Where(a => a.Situacao == 1).ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Usuario> CreateUsuario(UsuarioModel model)
        {
            var usuario = _imapper.Map<Usuario>(model);
            if (await this.FindByUsername(usuario.Login) != null)
            {
                LogicalException("There is already a registered user with the entered username.");
            }

            if (await _usuarioRepository.Table.Where(u => u.Email == usuario.Email).AnyAsync())
            {
                LogicalException("There is already a registered user with the email provided");
            }

            usuario = await _usuarioRepository.Insert(usuario);
            await _usuarioRepository.CommitAsync();
            return usuario;
        }
    }
}
