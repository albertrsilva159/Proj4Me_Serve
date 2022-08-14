using System;

namespace Proj4Me.Domain.Colaboradores.Commands
{
  public class AtualizarColaboradorCommand : BaseColaboradorCommand
  {
    public AtualizarColaboradorCommand(Guid id, string nome, string email)
    {
      Nome = nome;
      Email = email;
    }
  }
}
