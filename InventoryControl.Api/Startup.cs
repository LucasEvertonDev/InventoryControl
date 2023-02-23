using InventoryControl.Api.BackService;
using InventoryControl.Api.Factorys;
using InventoryControl.Api.Factorys.Interfaces;
using InventoryControl.Infra.IoC;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {  // Add services to the container.
        services.AddHostedService<MessageBackgroundService>();

        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen();

        services.AddInfraEstructure(Configuration);

        services.AddScoped<IMessageModelFactory, MessageModelFactory>();
        services.AddScoped<IServicoModelFactory, ServicoModelFactory>();
        services.AddScoped<IClienteModelFactory, ClienteModelFactory>();
        services.AddScoped<IAtendimentoModelFactory, AtendimentoModelFactory>();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}