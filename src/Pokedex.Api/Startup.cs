namespace Pokedex.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceCollection _services;

        public Startup(WebApplicationBuilder builder)
        {
            _configuration = builder.Configuration;
            _services = builder.Services;
        }

        public void ConfigureServices() //processar tudo que é services
        {
            _services.AddControllers();
            _services.AddEndpointsApiExplorer();
            _services.AddSwaggerGen(options => options.EnableAnnotations());
            _services.AddAutoMapper(GetType().Assembly);
        }

        public void ConfigureApplication(IApplicationBuilder app) //processar tudo que é app
        {

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(options => options.MapControllers());
        }
    }
}