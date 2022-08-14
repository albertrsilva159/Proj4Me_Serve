using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj4Me.Domain.ProjetosAreaServicos.Commands
{
  public class IncluirClienteProjetoAreaServicoCommand
  {
    public IncluirClienteProjetoAreaServicoCommand(Guid id, string nome, Guid? projetoAreaServico)
    {
      Id = id;
      Nome = nome;
      ProjetoAreaServicoId = projetoAreaServico;
    }

    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public Guid? ProjetoAreaServicoId { get; private set; }

  }
}
