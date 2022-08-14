

namespace Proj4Me.Domain.Colaboradores.Commands
{
  public class RegistrarColaboradorCommand : BaseColaboradorCommand
  {
    public RegistrarColaboradorCommand(string nome, string email)
    {
      Nome = nome;
      Email = email;
    }
  }
}
