using Proj4Me.Domain.Core.Commands;
using System;

namespace Proj4Me.Domain.ProjetosAreaServicos.Commands
{
  public abstract class BaseProjetoAreaServicoCommand : Command
  {
    public Guid Id { get; protected set; }
    public string Nome { get; protected set; }
    public string Descricao { get; protected set; }
    public Guid ColaboradorId { get; protected set; }
    public Guid PerfilId { get; protected set; }
    public Guid ClienteId { get; protected set; }
  }
}
