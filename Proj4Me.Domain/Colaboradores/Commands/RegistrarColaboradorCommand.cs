

using System;

namespace Proj4Me.Domain.Colaboradores.Commands
{
  public class RegistrarColaboradorCommand : BaseColaboradorCommand
  {
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }


    public RegistrarColaboradorCommand(Guid id, string nome, string email)
    {
      Id = id;
      Nome = nome;
      Email = email;
    }
  }
}
