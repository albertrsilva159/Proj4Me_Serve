using System;
using System.Numerics;

namespace Proj4Me.Domain.Clientes.Events
{
  public class ClienteAtualizadoEvent : BaseClienteEvent
  {
    public ClienteAtualizadoEvent(Guid id, string nome, int indexCliente)
    {
      Id = id;
      Nome = nome;
      IndexCliente = indexCliente;

      AggregateId = id; // precisa informar o aggregateid para a mensagem e todo mundo é uma mensagem
    }
  }
}