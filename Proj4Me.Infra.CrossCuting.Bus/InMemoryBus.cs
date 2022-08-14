using Proj4Me.Domain.Core.Bus;
using Proj4Me.Domain.Core.Commands;
using Proj4Me.Domain.Core.Events;
using System;
using Proj4Me.Domain.Core.Notification;

namespace Proj4Me.Infra.CrossCuting.Bus
{
  /// <summary>
  ///  classe selada pra nao ser herdada por ninguem
  /// </summary>
  public sealed class InMemoryBus : IBus
  {

    //Essa interface IServiceProvider é encontrada no system mas esta dentro de uma biblioteca de componente a parte que se chama System.ComponentModel, pra isso precisa instalar ela
    //Define um mecanismo para recuperar um objeto de serviço, ou seja, um objeto que dá suporte personalizado a outros objetos.
    //Essa manobra é pra se ter acesso aqui ao container de injecao de dependencia sem ao menos conhece-la
    public static Func<IServiceProvider> ContainerAccessor { get; set; }// Metodo de acesso ao container de injecao de dependencia e ele é uma função porque passamos para o IServiceProvider essa funçao como segue abaixo
    //Esse ContainerAccessor vai ser injetado diretamente no projeto aspnet no startup.cs. Pra isso adiciona o Proj4Me.Infra.CrossCuting.Bus na camda de apresentacao "Proj4Me.Web" e na camada "Proj4Me.Infra.CrossCuting.IoC" tambem incluir o Bus
    //Em seguida no startup da Proj4Me.Web ativar o bus e ajustar as dependencias. (InMemoryBus.ContainerAccessor = () => accessor.HttpContext.RequestServices;)
    private static IServiceProvider Container => ContainerAccessor();


    public void RaiseEvent<T>(T theEvent) where T : Event
    {
      Publish(theEvent);
    }

    public void SendCommand<T>(T theCommand) where T : Command
    {
      Publish(theCommand);
    }
    /// <summary>
    /// Message classe que esta no domain.core que define qualquer tipo de mensagem sendo evento ou command
    /// TODO: 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="message"></param>
    private static void Publish<T>(T message) where T : Message
    {
      if (Container == null) return;
      // Aqui como ele recebe uma mensagem como parametro e ele nao sabe que tipo de mensagem esta chegando, foi  entao definido que se for do tipo "DomainNotification" ele pega a injeção de dependencia que usa essa interface IDomainNotificationHandler
      // se nao pega a injecao de dependencia que usa a interface IHandler
      var obj = Container.GetService(message.MessageType.Equals("DomainNotification")
          ? typeof(IDomainNotificationHandler<T>)
          : typeof(IHandler<T>));

      //Atraves deste cast ele vai definir qual o tipo de IHandler ele vai chamar porque pode ter mais de um handle, como por exemplo de registrar, ou atualizar ou excluir.
      ((IHandler<T>)obj).Handle(message);
    }
  }
}
