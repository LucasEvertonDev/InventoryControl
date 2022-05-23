using AutoMapper;
using InventoryControl.Application.Interfaces;
using InventoryControl.Application.Models;
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
        private readonly IRepository<PerfilUsuario> _perfilUsuarioRepository;

        public IMapper _imapper { get; }

        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            IRepository<MapPerfilUsuariosAcessos> mapPerfilUsuariosAcessos,
            IRepository<PerfilUsuario> perfilUsuarioRepository,
            IMapper imapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapPerfilUsuariosAcessos = mapPerfilUsuariosAcessos;
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
            var perfis = await _perfilUsuarioRepository.FindAll();
            return perfis.Where(a => a.Situacao == 1).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Usuario> CreateUsuario(UsuarioModel model)
        {
            try
            {
                var usuario = _imapper.Map<Usuario>(model);
                if (await this.FindByUsername(usuario.Login) != null)
                {
                    LogicalException("There is already a registered user with the entered username.");
                }

                var itens = await _usuarioRepository.Itens;
                if (itens.Where(u => u.Email == usuario.Email).Any())
                {
                    LogicalException("There is already a registered user with the email provided");
                }

                usuario = await _usuarioRepository.Insert(usuario);
                await _usuarioRepository.Save();
                return usuario;
            }
            catch
            {
                throw;
            }
        }
    }
}
