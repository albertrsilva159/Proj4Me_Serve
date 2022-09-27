using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Proj4Me.Domain.Core.Events;
using Proj4Me.Domain.Core.Notification;
using Proj4Me.Domain.Interfaces;
using Proj4Me.Domain.ProjetosAreaServicos.Commands;
using Proj4Me.Domain.ProjetosAreaServicos.Events;
using Proj4Me.Domain.ProjetosAreaServicos.Repository;
using Proj4Me.Domain.Colaboradores.Repository;
using Proj4Me.Domain.Perfis.Repository;
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
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Proj4Me.Domain.Handlers;

namespace Proj4Me.Infra.CrossCutting.IoC
{
  public class NativeInjectorBootStrapper
  {
    public static void RegisterServices(IServiceCollection services)
    {
      //ASPNET
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      ///////////////services.AddSingleton(Mapper.Configuration);
      //services.AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService));


      // Domain Bus (Mediator)
      services.AddScoped(typeof(Mediator));
      services.AddScoped<IMediatorHandler, MediatorHandler>();

      ///Entao para cada comando ele vai chamar o CommandHandler
      //DOMAIN Commands
      services.AddScoped<IRequestHandler<RegistrarProjetoAreaServicoCommand>, ProjetoAreaServicoCommandHandler>();
      services.AddScoped<IRequestHandler<AtualizarProjetoAreaServicoCommand>, ProjetoAreaServicoCommandHandler>();
      services.AddScoped<IRequestHandler<ExcluirProjetoAreaServicoCommand>, ProjetoAreaServicoCommandHandler>();

      services.AddScoped<IRequestHandler<RegistrarColaboradorCommand>, ColaboradorCommandHandler>();
      services.AddScoped<IRequestHandler<AtualizarColaboradorCommand>, ColaboradorCommandHandler>();
      services.AddScoped<IRequestHandler<ExcluirColaboradorCommand>, ColaboradorCommandHandler>();

      services.AddScoped<IRequestHandler<RegistrarPerfilCommand>, PerfilCommandHandler>();
      services.AddScoped<IRequestHandler<AtualizarPerfilCommand>, PerfilCommandHandler>();
      services.AddScoped<IRequestHandler<ExcluirPerfilCommand>, PerfilCommandHandler>();
      /// ////////////services.AddScoped<IHandler<RegistrarClienteommand>, ClienteCommandHandler>();

      /// Para cada comando é dispardo um evento e o evento é resolvido pelo EventHandler
      //Domain - Eventos
      services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
      services.AddScoped<INotificationHandler<ProjetoAreaServicoRegistradoEvent>, ProjetoAreaServicoEventHandler>();
      services.AddScoped<INotificationHandler<ProjetoAreaServicoAtualizadoEvent>, ProjetoAreaServicoEventHandler>();
      services.AddScoped<INotificationHandler<ProjetoAreaServicoExcluidoEvent>, ProjetoAreaServicoEventHandler>();

      services.AddScoped<INotificationHandler<ColaboradorRegistradoEvent>, ColaboradorEventHandler>();
      services.AddScoped<INotificationHandler<ColaboradorAtualizadoEvent>, ColaboradorEventHandler>();
      services.AddScoped<INotificationHandler<ColaboradorExcluidoEvent>, ColaboradorEventHandler>();

      services.AddScoped<INotificationHandler<PerfilRegistradoEvent>, PerfilEventHandler>();
      services.AddScoped<INotificationHandler<PerfilAtualizadoEvent>, PerfilEventHandler>();
      services.AddScoped<INotificationHandler<PerfilExcluidoEvent>, PerfilEventHandler>();

      //Infra - Data
      services.AddScoped<IProjetoAreaServicoRepository, ProjetoAreaServicoRepository>();
      services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
      services.AddScoped<IPerfilRepository, PerfilRepository>();
      services.AddScoped<IServiceRepository, ServiceRepository>();

      services.AddScoped<IUnitOfWork, UnitOfWork>();

      // nao tem interface porque o context nao dipoe de interface
      services.AddSingleton<ProjetoAreaServicoContext>();
      //services.AddScoped<ProjetoAreaServicoContext>();

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
