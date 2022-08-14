using System;

namespace Proj4Me.Domain.ProjetosAreaServicos.Events
{
  public class ProjetoAreaServicoExcluidoEvent : BaseProjetoAreaServicoEvent
  {
    public ProjetoAreaServicoExcluidoEvent(Guid id)
    {
      Id = id;

      AggregateId = id; // precisa informar o aggregateid para a mensagem e todo mundo é uma mensagem
    }

  }
}
