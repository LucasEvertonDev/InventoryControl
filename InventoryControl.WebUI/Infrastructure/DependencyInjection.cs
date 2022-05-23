using InventoryControl.WebUI.Factories;
using InventoryControl.WebUI.Factories.Interfaces;
using InventoryControl.WebUI.Identity;
using InventoryControl.WebUI.Identity.Hasher;
using InventoryControl.WebUI.Identity.Stores;
using Microsoft.AspNetCore.Identity;

namespace InventoryControl.WebUI.Infrastructure
{
    public static class DependencyInjection
    {
        public static void IncludeWebUIDependencys(this IServiceCollection services)
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
        }
    }
}
