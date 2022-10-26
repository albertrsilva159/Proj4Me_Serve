using System;


namespace Proj4Me.Domain.Clientes.Events
{
  public class ClienteExcluidoEvent : BaseClienteEvent
  {
    public ClienteExcluidoEvent(Guid id)
    {
      Id = id;

      AggregateId = id; // precisa informar o aggregateid para a mensagem e todo mundo é uma mensagem
    }
  }
}