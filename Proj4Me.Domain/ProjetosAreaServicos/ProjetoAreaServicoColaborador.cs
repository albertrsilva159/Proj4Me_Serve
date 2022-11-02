using Proj4Me.Domain.Colaboradores;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proj4Me.Domain.ProjetosAreaServicos
{
  public class ProjetoAreaServicoColaborador
  {
    public Guid ProjetoAreaServicoId { get; set; }
    public ProjetoAreaServico ProjetoAreaServico { get; set; }

    public Guid ColaboradorId { get; set; }
    public Colaborador Colaborador { get; set; }
  }
}
