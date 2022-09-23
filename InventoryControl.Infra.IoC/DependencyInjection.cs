using AutoMapper;
using InventoryControl.Application.Interfaces;
using InventoryControl.Application.Mapping;
using InventoryControl.Application.Services;
using InventoryControl.Domain.Entities;
using InventoryControl.Domain.Interfaces;
using InventoryControl.Infra.Data.Context;
using InventoryControl.Infra.Data.Repository;
using InventoryControl.Infra.IoC.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WhatsApp.SimpleCRM.Infra.CrossCutting.InversionOfControl;

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

            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IAcessosService, AcessosService>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IServicosService, ServicosService>();
            services.AddScoped<IAtendimentoService, AtendimentoService>();
            services.AddScoped<ICustosService, CustosService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPasswordHashService, PasswordHashService>();

            services.AddScoped<IRepository<Usuario>, Repository<Usuario>>();
            services.AddScoped<IRepository<MapPerfilUsuariosAcessos>, Repository<MapPerfilUsuariosAcessos>>();
            services.AddScoped<IRepository<Acesso>, Repository<Acesso>>();
            services.AddScoped<IRepository<PerfilUsuario>, Repository<PerfilUsuario>>();
            services.AddScoped<IRepository<Cliente>, Repository<Cliente>>();
            services.AddScoped<IRepository<Servico>, Repository<Servico>>();
            services.AddScoped<IRepository<MapServicosAtendimento>, Repository<MapServicosAtendimento>>();
            services.AddScoped<IRepository<Atendimento>, Repository<Atendimento>>();
            services.AddScoped<IRepository<Custo>, Repository<Custo>>();


            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DomainToDTOMappingProfile());
            });

            services.AddRange(new ServiceCollection().AddInfraDependecies()
                                                    .AddRepositoryDependencies()
                                                    .AddServiceDependencies());

            services.AddSingleton(mapperConfig.CreateMapper());

            services.AddMemoryCache();

            return services;
        }
    }
}
