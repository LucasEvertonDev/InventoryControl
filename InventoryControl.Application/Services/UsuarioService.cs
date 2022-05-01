using InventoryControl.Application.Interfaces;
using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Application.Services
{
    public class UsuarioService : Service, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRepository<MapPerfilUsuariosAcessos> _mapPerfilUsuariosAcessos;

        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            IRepository<MapPerfilUsuariosAcessos> mapPerfilUsuariosAcessos)
        {
            _usuarioRepository = usuarioRepository;
            _mapPerfilUsuariosAcessos = mapPerfilUsuariosAcessos;
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
            var users = await _usuarioRepository.FindAll();
            return users.Where(a => a.Nome == name).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="perfilUsuario"></param>
        /// <returns></returns>
        public async Task<List<Acesso>> FindAcessosByPerfilUsuarioId(int perfilUsuario)
        {
            var acessos = await _usuarioRepository.FindAcessosByPerfilUsuarioId(perfilUsuario);
            return acessos;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task<Usuario> FindByUsername(string userName)
        {
            var users = await _usuarioRepository.FindAll();
            return users.Where(a => a.Login.ToLower() == userName.ToLower()).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Usuario> Login(string userName, string password)
        {
            var user = await this.FindByUsername(userName);
            if (user == null || user.Id < 1)
            {
                LogicalException("Username não cadastrado!");
            }

            if (!user.Senha.Equals(password))
            {
                LogicalException("Password inválido!");
            }
            return user;
        }
    }
}
