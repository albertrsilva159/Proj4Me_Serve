using System;

namespace Proj4Me.Domain.Clientes.Commands
{
  public class AtualizarClienteCommand : BaseClienteCommand
  {
    public AtualizarClienteCommand(Guid id, int indexClienteProj4Me, string nome, string email)
    {
      Nome = nome;
      IndexClienteProj4Me = indexClienteProj4Me;
      

    }
  }
}
