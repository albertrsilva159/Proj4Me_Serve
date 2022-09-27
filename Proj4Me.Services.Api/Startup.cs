using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Proj4Me.Infra.CrossCutting.AspNetFilters;
using Proj4Me.Infra.CrossCutting.Bus;
using Proj4Me.Infra.CrossCutting.Identity.Data;
using Proj4Me.Infra.CrossCutting.Identity.Models;
using Proj4Me.Infra.CrossCutting.IoC;
using Proj4Me.Services.Api.Configurations;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.OpenApi.Models;
using Proj4Me.Services.Api.Middlewares;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Proj4Me.Infra.CrossCutting.Identity.Authorization;
using Newtonsoft.Json.Serialization;
using Microsoft.IdentityModel.Logging;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AutoMapper;

namespace Proj4Me.Services.Api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      // Add Framework services
      services.AddDbContext<ApplicationDbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
      //services.AddControllersWithViews();

      // Add application services
      services.AddIdentity<ApplicationUser, IdentityRole>()
              .AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();

      services.AddOptions();
      services.AddAutoMapper(typeof(Startup));
      services.AddMvc(options =>
      {
        options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());// a api so vai receber json
        options.UseCentralRoutePrefix(new RouteAttribute("api/v{version}"));

        // politica de autorizacao
        var policy = new AuthorizationPolicyBuilder()
                   .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                   .RequireAuthenticatedUser()
                   .Build();

        options.Filters.Add(new AuthorizeFilter(policy)); // todo filtro de autorização precisa ter o json web token jwt, ou seja onde tiver o [Authorize] precisa ter o token pra acessar
        options.Filters.Add(new ServiceFilterAttribute(typeof(GlobalActionLogger)));
      });

      services.AddAuthorization(options =>
      {
        options.AddPolicy("Ler", policy => policy.RequireClaim("Projetos", "Ler"));
        options.AddPolicy("PodeGravar", policy => policy.RequireClaim("Projetos", "Gravar"));
      });
      
      services.Configure<JwtTokenOptions>(Configuration.GetSection("JwtTokenOptions"));

      var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtTokenOptions));
      var key = Encoding.ASCII.GetBytes("EventosIoTokenServer");  

      services.AddAuthentication(x =>
      {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
            .AddJwtBearer(x =>
            {
              x.RequireHttpsMetadata = false;
              x.SaveToken = true;
              x.TokenValidationParameters = new TokenValidationParameters
              {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),

                ValidateIssuer = true,
                ValidateAudience = false,
                RequireExpirationTime = true,
                ValidateLifetime = true,

                ValidIssuer = jwtAppSettingOptions[nameof(JwtTokenOptions.Issuer)],  //Configuration["JJwtTokenOptionswt:Issuer"], ///"InventarioNeTAuthenticationServer", //Configuration["Jwt:Issuer"], ///jwtAppSettingOptions[nameof(JwtTokenOptions.Issuer)],                
                ValidAudience = jwtAppSettingOptions[nameof(JwtTokenOptions.Audience)],

                ClockSkew = TimeSpan.Zero
              };
            });

      services.AddSwaggerGen(options =>
      {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
          Version = "v1",
          Title = "Proj4Me",
          Description = "Controle de gerenciamento de projeto"
        });
      });
   
      services.AddAutoMapper(typeof(Startup)); 

      services.AddControllers().AddNewtonsoftJson(options =>
                                                  { options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                                                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                                                   }); 
    RegisterServices(services);
    }

    public void Configure(IApplicationBuilder app,
                              IHostingEnvironment env,
                              ILoggerFactory loggerFactory,
                              IHttpContextAccessor accessor)
    {

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();
      app.UseRouting();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
  
      app.UseCors(c =>
      {
        c.AllowAnyHeader();
        c.AllowAnyMethod();
        c.AllowAnyOrigin();
      });

      app.UseAuthentication();
      app.UseAuthorization();      

      app.UseSwagger();
      app.UseSwaggerUI(s =>
      {
        s.SwaggerEndpoint("/swagger/v1/swagger.json", "Proj4Me v1");// caminho para chegar no swagger
      });

      InMemoryBus.ContainerAccessor = () => accessor.HttpContext.RequestServices;
    }

    private static void RegisterServices(IServiceCollection services)
    {
      NativeInjectorBootStrapper.RegisterServices(services);
    }    
  }
}
