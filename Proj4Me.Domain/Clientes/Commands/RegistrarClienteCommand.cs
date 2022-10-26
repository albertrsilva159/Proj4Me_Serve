

using System;

namespace Proj4Me.Domain.Clientes.Commands
{
  public class RegistrarClienteCommand : BaseClienteCommand
  {
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public int  IndexClienteProj4Me { get; private set; }


    public RegistrarClienteCommand(Guid id, string nome, int indexClienteProj4Me)
    {
      Id = id;
      Nome = nome;
      IndexClienteProj4Me = indexClienteProj4Me;
    }
  }
}
