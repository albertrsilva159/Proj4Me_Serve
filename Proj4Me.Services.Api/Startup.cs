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
      // Contexto do EF para o Identity
      services.AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      // Configurações de Autenticação, Autorização e JWT.
      services.AddMvcSecurity(Configuration);

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
      services.AddMediatR(typeof(Startup));

      // Registrar todos os DI
      services.AddDIConfiguration();
    }

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

      app.UseCors(c =>
      {
        c.AllowAnyHeader();
        c.AllowAnyMethod();
        c.AllowAnyOrigin();
      });
 app.UseHttpsRedirection();
 app.UseRouting();
      app.UseStaticFiles();
      app.UseAuthentication();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });


      //app.UseMvc();      

      #endregion

      #region Swagger

      if (env.IsProduction())
      {
        // Se não tiver um token válido no browser não funciona.
        // Descomente para ativar a segurança.
        // app.UseSwaggerAuthorized();
      }


     
     
  
     
      
      app.UseSwagger();
      app.UseSwaggerUI(s =>
      {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Proj4Me v1");// caminho para chegar no swagger
      });

      #endregion
    }
  }
}
