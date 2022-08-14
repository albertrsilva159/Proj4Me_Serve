using Proj4Me.Domain.Core.Events;
using System;

namespace Proj4Me.Domain.ProjetosAreaServicos.Events
{
  public abstract class BaseProjetoAreaServicoEvent : Event
  {
    public Guid Id { get; protected set; }
    public string Nome { get; protected set; }
    public string Descricao { get; protected set; }
    public string DescricaoLonga { get; protected set; }
  }
}
