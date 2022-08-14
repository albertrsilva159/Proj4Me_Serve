using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Proj4Me.Web.Data;
using Proj4Me.Application.Interfaces;
using Proj4Me.Application.Services;
using Microsoft.AspNetCore.Http;
using Proj4Me.Infra.CrossCuting.Bus;
using Proj4Me.Infra.CrossCuting.IoC;
using AutoMapper;

namespace Proj4Me.Web
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

      
      services.AddControllersWithViews();

        services.AddDbContext<Proj4MeWebContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      services.AddScoped<IProjetoAreaServicoAppService, ProjetoAreaServicoAppService>();
      
      RegisterServices(services);
      services.AddAutoMapper(typeof(Startup));
      //services.AddAutoMapper();
      services.AddMvcCore();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, 
                          IWebHostEnvironment env,
                          IHttpContextAccessor accessor)// IHttpContextAccessor é o acesso ao nosso contexto http, 
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }
      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
      });

      //Dentro do RequestServices esta as definições das injecoes de dependencias, entao atraves do IServiceProvider (ContainerAccessor) é possivel chamar o mecanimos do asp.net core de injeçao de dependencia e falar que quer a instancia de determinado objeto
      //com base na tabela que foi criada no NativeInjectorBootStrapper
      InMemoryBus.ContainerAccessor = () => accessor.HttpContext.RequestServices;

    }
    //É preciso add Proj4Me.Infra.CrossCuting.IoC
    //Da mesma forma que foi configurado aqui se tiver outro projeto basta no startup criar a mesma configuracao
    private static void RegisterServices(IServiceCollection services)
    {
      NativeInjectorBootStrapper.RegisterServices(services);
    }
  }
}
