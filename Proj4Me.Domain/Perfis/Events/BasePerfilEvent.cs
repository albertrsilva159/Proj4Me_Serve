using Proj4Me.Domain.Core.Events;
using System;

namespace Proj4Me.Domain.Perfis.Events
{
  public class BasePerfilEvent : Event
  {
    public Guid Id { get; protected set; }
    public string Nome { get; protected set; }

  }
}
