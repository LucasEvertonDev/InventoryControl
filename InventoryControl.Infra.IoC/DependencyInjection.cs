using AutoMapper;
using InventoryControl.Application.Interfaces;
using InventoryControl.Application.Mapping;
using InventoryControl.Application.Services;
using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces;
using InventoryControl.Infra.Data.Context;
using InventoryControl.Infra.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryControl.Infra.IoC
{
    public static class DependencyInjection
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfraEstructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IRepository<Usuario>, Repository<Usuario>>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IAcessosService, AcessosService>();
            services.AddScoped<IRepository<MapPerfilUsuariosAcessos>, Repository<MapPerfilUsuariosAcessos>>();
            services.AddScoped<IRepository<Acesso>, Repository<Acesso>>();
            services.AddScoped<IRepository<PerfilUsuario>, Repository<PerfilUsuario>>();


            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DomainToDTOMappingProfile());
            });

            services.AddSingleton(mapperConfig.CreateMapper());

            services.AddMemoryCache();

            return services;
        }
    }
}
