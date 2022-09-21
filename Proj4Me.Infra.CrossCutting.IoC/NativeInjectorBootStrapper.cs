using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Proj4Me.Application.AutoMapper;
using Proj4Me.Application.Interfaces;
using Proj4Me.Application.Services;
using Proj4Me.Domain.Core.Bus;
using Proj4Me.Domain.Core.Events;
using Proj4Me.Domain.Core.Notification;
using Proj4Me.Domain.Interfaces;
using Proj4Me.Domain.ProjetosAreaServicos.Commands;
using Proj4Me.Domain.ProjetosAreaServicos.Events;
using Proj4Me.Domain.ProjetosAreaServicos.Repository;
using Proj4Me.Domain.Colaboradores.Repository;
using Proj4Me.Domain.Perfis.Repository;
using Proj4Me.Infra.CrossCutting.Bus;
using Proj4Me.Infra.Data.Context;
using Proj4Me.Infra.Data.Repository;
using Proj4Me.Infra.Data.UoW;
using Microsoft.AspNetCore.Http;
using Proj4Me.Domain.Colaboradores.Commands;
using Proj4Me.Domain.Perfis.Commands;
using Proj4Me.Domain.Colaboradores.Events;
using Proj4Me.Domain.Perfis.Events;
using Proj4Me.Infra.Service.Interfaces;
using Proj4Me.Infra.Service.Services;
using Proj4Me.Infra.CrossCutting.Identity.Services;
using Proj4Me.Infra.CrossCutting.Identity.Models;
using Microsoft.Extensions.Logging;
using Proj4Me.Infra.CrossCutting.AspNetFilters;

namespace Proj4Me.Infra.CrossCutting.IoC
{
  public class NativeInjectorBootStrapper
  {
    public static void RegisterServices(IServiceCollection services)
    {
      //ASPNET
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      //Application
      services.AddSingleton<IConfigurationProvider>(AutoMapperConfiguration.RegisterMappings());
      services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));
      services.AddScoped<IProjetoAreaServicoAppService, ProjetoAreaServicoAppService>();
      services.AddScoped<IColaboradorAppService, ColaboradorAppService>();
      services.AddScoped<IPerfilAppService, PerfilAppService>();
      ///Entao para cada comando ele vai chamar o CommandHandler
      //DOMAIN Commands
      services.AddScoped<IHandler<RegistrarProjetoAreaServicoCommand>, ProjetoAreaServicoCommandHandler>();
      services.AddScoped<IHandler<AtualizarProjetoAreaServicoCommand>, ProjetoAreaServicoCommandHandler>();
      services.AddScoped<IHandler<ExcluirProjetoAreaServicoCommand>, ProjetoAreaServicoCommandHandler>();

      services.AddScoped<IHandler<RegistrarColaboradorCommand>, ColaboradorCommandHandler>();
      services.AddScoped<IHandler<AtualizarColaboradorCommand>, ColaboradorCommandHandler>();
      services.AddScoped<IHandler<ExcluirColaboradorCommand>, ColaboradorCommandHandler>();

      services.AddScoped<IHandler<RegistrarPerfilCommand>, PerfilCommandHandler>();
      services.AddScoped<IHandler<AtualizarPerfilCommand>, PerfilCommandHandler>();
      services.AddScoped<IHandler<ExcluirPerfilCommand>, PerfilCommandHandler>();
      /// ////////////services.AddScoped<IHandler<RegistrarClienteommand>, ClienteCommandHandler>();

      /// Para cada comando é dispardo um evento e o evento é resolvido pelo EventHandler
      //Domain - Eventos
      services.AddScoped<IDomainNotificationHandler<DomainNotification>, DomainNotificationHandler>();
      services.AddScoped<IHandler<ProjetoAreaServicoRegistradoEvent>, ProjetoAreaServicoEventHandler>();
      services.AddScoped<IHandler<ProjetoAreaServicoAtualizadoEvent>, ProjetoAreaServicoEventHandler>();
      services.AddScoped<IHandler<ProjetoAreaServicoExcluidoEvent>, ProjetoAreaServicoEventHandler>();

      services.AddScoped<IHandler<ColaboradorRegistradoEvent>, ColaboradorEventHandler>();
      services.AddScoped<IHandler<ColaboradorAtualizadoEvent>, ColaboradorEventHandler>();
      services.AddScoped<IHandler<ColaboradorExcluidoEvent>, ColaboradorEventHandler>();

      services.AddScoped<IHandler<PerfilRegistradoEvent>, PerfilEventHandler>();
      services.AddScoped<IHandler<PerfilAtualizadoEvent>, PerfilEventHandler>();
      services.AddScoped<IHandler<PerfilExcluidoEvent>, PerfilEventHandler>();

      //Infra - Data
      services.AddScoped<IProjetoAreaServicoRepository, ProjetoAreaServicoRepository>();
      services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
      services.AddScoped<IPerfilRepository, PerfilRepository>();
      services.AddScoped<IServiceRepository, ServiceRepository>();

      services.AddScoped<IUnitOfWork, UnitOfWork>();

      // nao tem interface porque o context nao dipoe de interface
      services.AddSingleton<ProjetoAreaServicoContext>();
      services.AddScoped<ProjetoAreaServicoContext>();

      //Infra Bus
      services.AddScoped<IBus, InMemoryBus>();

      // Infra - Identity
      services.AddTransient<IEmailSender, AuthMessageSender>();
      services.AddTransient<ISmsSender, AuthMessageSender>();
      services.AddScoped<IUser, AspNetUser>();

      // Infra - Filtros de log
      services.AddScoped<ILogger<GlobalExceptionHandlingFilter>, Logger<GlobalExceptionHandlingFilter>>();
      services.AddScoped<ILogger<GlobalActionLogger>, Logger<GlobalActionLogger>>();
      services.AddScoped<GlobalExceptionHandlingFilter>();
      services.AddScoped<GlobalActionLogger>();
    }
  }
}
