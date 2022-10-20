using System;
using AutoMapper;
using Proj4Me.Infra.CrossCutting.AspNetFilters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Proj4Me.Infra.CrossCutting.Identity.Data;
using Proj4Me.Services.Api.Configurations;
using Proj4Me.Services.Api.Middlewares;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Reflection;
using Microsoft.AspNetCore.Server.IISIntegration;
using Proj4Me.Services.Api.AutoMapper;
using Proj4Me.Domain.Handlers;
using Proj4Me.Domain.Interfaces;

namespace Proj4Me.Services.Api
{
  public class Startup
  {
    public Startup(IHostingEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
          .AddEnvironmentVariables();
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {

      services.AddCors(options =>
      {
        options.AddPolicy(name: "CorsApi",
            builder =>
            {
              builder.WithOrigins("https://localhost:44351", "http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod();
            });
      });

      // Contexto do EF para o Identity
      services.AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      // Configurações de Autenticação, Autorização e JWT.
      services.AddMvcSecurity(Configuration);

    

      //services.AddControllers();


      #region comentarios
      //services.AddCors(options =>
      //{
      //  options.AddDefaultPolicy(builder => builder.WithOrigins());
      //  options.AddPolicy(
      //    "CorsPolicy",
      //    builder => builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
      //    .AllowAnyMethod()
      //    .AllowAnyHeader());
      //});

 //services.AddCors(options => {
      //  options.AddPolicy("CorsApi", builder => builder
      //  .WithOrigins("http://localhost:4200")
      //  .SetIsOriginAllowed((host) => "http://localhost:4200".Equals(host, StringComparison.InvariantCultureIgnoreCase))
      //  .AllowAnyMethod()
      //  .AllowAnyHeader());
      //});

      //services.AddCors(options =>
      //{
      //  options.AddPolicy("CorsApi",
      //      builder => builder.WithOrigins("http://localhost:4200")
      //          .AllowAnyHeader()
      //          .AllowAnyMethod());
      //});

      //services.AddCors(options =>
      //{
      //  options.AddPolicy(
      //    name: "porracors",
      //    builder =>
      //    {
      //      builder.WithOrigins("http://localhost:4200")
      //      .WithMethods("POST","GET","OPTIONS","PUT","PATH","DELETE")
      //      .WithHeaders("Content-Type", "Origin", "Authorization", "X-Auth-Token")
      //      .WithExposedHeaders("Cache-Control", "Content-Language", "Content-Type", "Expires", "Last-Modified", "Pragma");
      //    });

      //});

      //services.AddCors(options =>
      //{
      //  options.AddDefaultPolicy(builder => builder.AllowAnyOrigin()
      //    .AllowAnyMethod()
      //    .AllowAnyHeader());
      //});



      //services.AddCors(options =>  
      //{
      //  options.AddPolicy(
      //    "CorsPolicy",
      //    builder => builder.WithOrigins("http://localhost:4200")
      //    .AllowAnyMethod()
      //    .AllowAnyHeader()
      //    .AllowCredentials());
      //});

      //services.AddCors(options =>
      //{
      //  options.AddPolicy("CorsPolicy",
      //      builder => builder
      //          .AllowAnyMethod()
      //          .AllowCredentials()
      //          .SetIsOriginAllowed((host) => true)
      //          .AllowAnyHeader());
      //});

      ///services.AddMediatR(Assembly.GetExecutingAssembly());
      //services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
      // services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

      //services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
      //services.AddMediatR(typeof(myAssemblyStuff).GetTypeInfo().Assembly);


      #endregion

      services.AddAuthentication(IISDefaults.AuthenticationScheme);

  // Options para configurações customizadas
      services.AddOptions();

      // MVC com restrição de XML e adição de filtro de ações.
      services.AddMvc(options =>
      {
        options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
        options.Filters.Add(new ServiceFilterAttribute(typeof(GlobalActionLogger)));
      });

  

      // Versionamento do WebApi
      services.AddApiVersioning("api/v{version}");

      // AutoMapper
      
      services.AddAutoMapper(typeof(Startup));

      // Configurações do Swagger
      services.AddSwaggerConfig();

      // MediatR
      //services.AddMediatR(typeof(Startup));
      services.AddMediatR(typeof(MediatorHandler));
     // services.AddMediatR(Assembly.GetExecutingAssembly());
     // services.AddScoped<IMediatorHandler, MediatorHandler>();
  
      //services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
      // Registrar todos os DI
      services.AddDIConfiguration();
    }

    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////// configure /////////////////////////////////////////////////////////////////////////////////////////////

    public void Configure(IApplicationBuilder app,
                          IHostingEnvironment env,
                          ILoggerFactory loggerFactory,
                          IHttpContextAccessor accessor)
    {

      #region Logging

      //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
      //loggerFactory.AddDebug();


      #endregion

      #region Configurações MVC

  
      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseRouting();       
      app.UseCors("CorsApi");
      //app.UseCors(x =>
      //{
      //  x.AllowAnyOrigin()
      //      .AllowAnyMethod()
      //      .AllowAnyHeader();
      //});
      app.UseAuthentication();
      app.UseAuthorization();
      
      app.UseSwagger();
      app.UseSwaggerUI(s =>
      {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Proj4Me v1");
      });

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
      
      // COMENTADO POR CAUSA DO CORS SEGUNDO INDICACOES/////////////////////app.UseHttpsRedirection();
      
      #region comentarios
      //app.UseCors("CorsPolicy");
      //app.UseCors("CorsApi");

      //app.UseCors(builder => builder.AllowAnyOrigin()
      //     .AllowAnyMethod()
      //     .AllowAnyHeader());


      //app.UseCors(c =>
      //{
      //  c.WithOrigins("*");
      //  //c.AllowAnyOrigin("*");
      //  c.AllowAnyHeader();
      //  c.AllowAnyMethod();

      //});
      //app.UseCors("MyPolicy");
      // app.UseCors("CorsPolicy");

      //app.UseCors("CorsPolicy");

      #endregion
        
      //app.UseMvc();
   
      #endregion

      #region Swagger

      //if (env.IsProduction())
      //{
      //  // Se não tiver um token válido no browser não funciona.
      //  // Descomente para ativar a segurança.
      //  // app.UseSwaggerAuthorized();
      //}

      #endregion
    }
  }
}
