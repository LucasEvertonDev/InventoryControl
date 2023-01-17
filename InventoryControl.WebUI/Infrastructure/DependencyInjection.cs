using AutoMapper;
using AWASP.WebUI.Data.Contexts;
using AWASP.WebUI.Data.Domains;
using AWASP.WebUI.Data.Repositorys;
using AWASP.WebUI.Data.Repositorys.Interfaces;
using AWASP.WebUI.Services;
using AWASP.WebUI.Services.Interfaces;
using AWASP.WebUI.Mapping;
using AWASP.WebUI.Services.Services;
using AWASP.WebUI.Factories;
using AWASP.WebUI.Factories.Interfaces;
using AWASP.WebUI.Identity;
using AWASP.WebUI.Identity.Hasher;
using AWASP.WebUI.Identity.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AWASP.WebUI.Infrastructure
{
    public static class DependencyInjection
    {
        public static void IncludeWebUIDependencys(this IServiceCollection services, IConfiguration configuration)
        {
            services
               .AddIdentity<ApplicationUser, ApplicationRole>()
               .AddRoles<ApplicationRole>()
               .AddDefaultTokenProviders();

            services.AddRazorPages().AddRazorRuntimeCompilation();

            // Identity Services
            services.AddTransient<IUserStore<ApplicationUser>, UserStore>();
            services.AddTransient<IRoleStore<ApplicationRole>, RoleStore>();
            services.AddScoped<IPasswordHasher<ApplicationUser>, NoPasswordHasher>();

            services.AddScoped<IUsuarioModelFactory, UsuarioModelFactory>();


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
        }
    }
}
