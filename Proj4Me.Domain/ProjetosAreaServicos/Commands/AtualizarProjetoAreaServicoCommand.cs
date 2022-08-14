using Proj4Me.Domain.Core.Commands;
using System;

namespace Proj4Me.Domain.ProjetosAreaServicos.Commands
{
  public class AtualizarProjetoAreaServicoCommand : BaseProjetoAreaServicoCommand
  {
    public AtualizarProjetoAreaServicoCommand(Guid id, string nome, string descricao, Guid colaboradorId, Guid perfilId)
    {
      Nome = nome;
      Descricao = descricao;
      Id = id;
      ColaboradorId = colaboradorId;
      PerfilId = perfilId;
    }
  }
}
