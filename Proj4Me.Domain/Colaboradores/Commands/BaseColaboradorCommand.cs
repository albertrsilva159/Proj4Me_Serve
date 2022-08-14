using Proj4Me.Domain.Core.Commands;
using System;

namespace Proj4Me.Domain.Colaboradores.Commands
{
  public  abstract class BaseColaboradorCommand : Command
  {
    public Guid Id { get; protected set; }
    public string Nome { get; protected set; }
    public string Email { get; protected set; }

  }
}
