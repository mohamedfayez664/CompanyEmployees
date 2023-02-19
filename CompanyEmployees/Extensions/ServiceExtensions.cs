using Contracts;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service;
using Service.Contracts;

namespace CompanyEmployees.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
            => services.AddCors(options =>
         {
             options.AddPolicy("CorsPolicy", builder =>
             builder.AllowAnyOrigin()  // o.WithOrigin("url:port//.com","  ",...);
             .AllowAnyMethod()  //WithMethods("POST", "GET") 
             .AllowAnyHeader());  // WithHeaders("accept", "contenttype")
         });


        /// IIS

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
                services.Configure<IISOptions>(options =>
        {

        });


        ///logger
        public static void ConfigureLoggerService(this IServiceCollection services) =>
                services.AddSingleton<ILoggerManager, LoggerManager>();


        //RepositoryManager
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
                  services.AddScoped<IRepositoryManager, RepositoryManager>();


        //ServiceManager
        public static void ConfigureServiceManager(this IServiceCollection services) =>
                  services.AddScoped<IServiceManager, ServiceManager>();

        //
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
             services.AddDbContext<RepositoryContext>(opts =>
                      opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));


        //public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
        //    services.AddSqlServer<RepositoryContext>((configuration.GetConnectionString("sqlConnection")));



    }
}


