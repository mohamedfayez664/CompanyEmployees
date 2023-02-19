using CompanyEmployees.Extensions;
using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;


var builder = WebApplication.CreateBuilder(args); //WebApplicationBuilder

// Add services to the container.(ConfigureServices )
//

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),
"/nlog.config"));

//var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
//LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),
//$"/nlog.{environment}.config "));
builder.Services.ConfigureSqlContext(builder.Configuration);

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();

builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();

////

builder.Services.AddControllers()
    .AddApplicationPart(typeof(CompanyEmployees.Presentation.AssemblyReference).Assembly);

var app = builder.Build();   ////WebApplication>> (Configure)
                             //IHost, IApplicationBuilder, IEndpointRouteBuilder
                             // Configure the HTTP request pipeline.  //middleware
///
//if (app.Environment.IsDevelopment())
//    app.UseDeveloperExceptionPage();
//else
//    app.UseHsts();
/*
adds a header Strict-Transport-Security to the response. When the site was accessed using HTTPS then 
the browser notes it down and future request using HTTP will be redirected to HTTPS. So, accessing the 
site using HTTPS at least once is mandatory to make this work.Also the expiration time set by the 
Strict-Transport-Security header elapses, the next attempt to load the site via HTTP won't be automatically redirected to HTTPS.
 */

/////

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);
if (app.Environment.IsProduction())
    app.UseHsts();







app.UseHttpsRedirection();  //redirection from HTTP to HTTPS

/////
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All  //forward proxy headers to the current request
});
app.UseCors("CorsPolicy");
//////
app.UseAuthorization();

//app.MapControllers();  //to>>  IEndpointRouteBuilder
app.MapControllers();  //to>>  IEndpointRouteBuilder


//app.Use(async (context, next) =>
//{
//    Console.WriteLine($"Logic before executing the next delegate in the Use method");
//    await next.Invoke();
//    Console.WriteLine($"Logic after executing the next delegate in the Use method");
//});


app.Run();
//app.Run(async context =>
//{
//    Console.WriteLine($"Writing the response to the client in the Run method");
//    await context.Response.WriteAsync("Hello from the middleware component.");
//});


//app.Run();
