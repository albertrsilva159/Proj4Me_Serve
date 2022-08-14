using Proj4Me.Domain.Colaboradores;
using Proj4Me.Domain.Colaboradores.Repository;
using Proj4Me.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Proj4Me.Infra.Data.Repository
{
  public class ColaboradorRepository : Repository<Colaborador>, IColaboradorRepository
  {

    public ColaboradorRepository(ProjetoAreaServicoContext context) : base(context)
    {

    }

    public IEnumerable<Colaborador> ObterColaborador(Guid colaboradorId)
    {
      return Db.Colaborador.Where(p => p.ColaboradorId.Equals(colaboradorId));
    }
  }
}
