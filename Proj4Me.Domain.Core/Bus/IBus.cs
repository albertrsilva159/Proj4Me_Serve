using Proj4Me.Domain.Core.Commands;
using Proj4Me.Domain.Core.Events;
using System;

namespace Proj4Me.Domain.Core.Bus
{
  /// <summary>
  /// basicament o bus realiza o disparo de eventos e comandos, ele diferencia um do outro atraves da classe base
  /// </summary>
  public interface IBus
  {
    // um comando é sempre lançado de uma camada para a outra
    void SendCommand<T>(T theCommand) where T : Command;
    // já um evento é sempre lançado para que faça algum efeito como por exemplo persistencia, notificacao e outros
    void RaiseEvent<T>(T theEvent) where T : Event;
  }
}
