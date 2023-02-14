using Contracts;
using LoggerService;
using Microsoft.AspNetCore.HttpOverrides;

namespace CompanyEmployees.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) 
            => services.AddCors  (options =>
         {
        options.AddPolicy  ("CorsPolicy", builder =>
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


    }
}


