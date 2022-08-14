using System;


namespace Proj4Me.Domain.Colaboradores.Events
{
  public class ColaboradorExcluidoEvent : BaseColaboradorEvent
  {
    public ColaboradorExcluidoEvent(Guid id)
    {
      Id = id;

      AggregateId = id; // precisa informar o aggregateid para a mensagem e todo mundo é uma mensagem
    }
  }
}