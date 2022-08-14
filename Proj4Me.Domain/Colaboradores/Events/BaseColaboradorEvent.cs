using Proj4Me.Domain.Core.Events;
using System;

namespace Proj4Me.Domain.Colaboradores.Events
{
  public class BaseColaboradorEvent : Event
  {
    public Guid Id { get; protected set; }
    public string Nome { get; protected set; }
    public string Descricao { get; protected set; }

  }
}
