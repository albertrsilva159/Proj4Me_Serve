using Proj4Me.Domain.Core.Events;
using System;

namespace Proj4Me.Domain.ProjetosAreaServicos.Events
{
  /// <summary>
  /// a ideia é fazer um tipo log da informacao, a ideia é gravar o estada da entidade saber quando foi criado, excluido ou alterado
  /// </summary>
  public class ProjetoAreaServicoEventHandler :
    IHandler<ProjetoAreaServicoRegistradoEvent>,
    IHandler<ProjetoAreaServicoAtualizadoEvent>,
    IHandler<ProjetoAreaServicoExcluidoEvent>
  {
    public void Handle(ProjetoAreaServicoExcluidoEvent message)
    {
      // enviar email
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("Evento excluir com sucesso!");

    }

    public void Handle(ProjetoAreaServicoAtualizadoEvent message)
    {
      // enviar email
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("Evento atualizar com sucesso!");
    }

    public void Handle(ProjetoAreaServicoRegistradoEvent message)
    {
      // enviar email
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("Evento registrar com sucesso!");
    }
  }
}
