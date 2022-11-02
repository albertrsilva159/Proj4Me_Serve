using Proj4Me.Domain.Core.Events;
using System;
using System.Numerics;

namespace Proj4Me.Domain.Clientes.Events
{
  public class BaseClienteEvent : Event
  {
    public Guid Id { get; protected set; }
    public string Nome { get; protected set; }
    public int IndexCliente { get; protected set; }

  }
}
