namespace InventoryControl.WebUI.Config
{
    public static class RouteConfig
    {
        public static void ConfigurateRoutes(this WebApplication app)
        {
            app.UseEndpoints(endpoints =>
            {
                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"); ;
            });
        }
    }
}
