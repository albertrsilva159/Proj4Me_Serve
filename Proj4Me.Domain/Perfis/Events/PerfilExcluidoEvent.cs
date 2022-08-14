using System;


namespace Proj4Me.Domain.Perfis.Events
{
  public class PerfilExcluidoEvent : BasePerfilEvent
  {
    public PerfilExcluidoEvent(Guid id)
    {
      Id = id;

      AggregateId = id; // precisa informar o aggregateid para a mensagem e todo mundo é uma mensagem
    }
  }
}