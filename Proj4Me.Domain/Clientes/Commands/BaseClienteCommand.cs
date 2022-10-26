using Proj4Me.Domain.Core.Commands;
using System;

namespace Proj4Me.Domain.Clientes.Commands
{
  public  abstract class BaseClienteCommand : Command
  {
    public Guid Id { get; protected set; }
    public int IndexClienteProj4Me { get; protected set; }
    public string Nome { get; protected set; }

  }
}
