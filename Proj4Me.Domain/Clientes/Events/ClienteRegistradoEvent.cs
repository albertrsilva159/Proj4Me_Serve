using Proj4Me.Domain.Clientes.Events;
using System;
using System.Numerics;

namespace Proj4Me.Domain.Clientes.Events
{
  public class ClienteRegistradoEvent : BaseClienteEvent
  {


    public ClienteRegistradoEvent(Guid id, string nome, int indexCliente)
    {
      Id = id;
      Nome = nome;
      IndexCliente = indexCliente;      

      AggregateId = id; // precisa informar o aggregateid para a mensagem e todo mundo é uma mensagem
    }
  }
}