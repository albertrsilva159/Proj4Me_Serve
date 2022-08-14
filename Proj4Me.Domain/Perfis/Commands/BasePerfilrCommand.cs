using Proj4Me.Domain.Core.Commands;
using System;

namespace Proj4Me.Domain.Perfis.Commands
{
  public abstract class BasePerfilCommand : Command
  {
    public Guid Id { get; protected set; }
    public string Nome { get; protected set; }
  }
}
