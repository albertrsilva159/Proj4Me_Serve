using System;

namespace Proj4Me.Domain.Perfis.Commands
{
  public class AtualizarPerfilCommand : BasePerfilCommand
  {
    public AtualizarPerfilCommand(Guid id, string nome)
    {
      Nome = nome;

    }
  }
}
