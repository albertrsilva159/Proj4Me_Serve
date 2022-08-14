using Proj4Me.Domain.Perfis;
using Proj4Me.Domain.Perfis.Repository;
using Proj4Me.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj4Me.Infra.Data.Repository
{
  public class PerfilRepository : Repository<Perfil>, IPerfilRepository
  {

    public PerfilRepository(ProjetoAreaServicoContext context) : base(context)
    {

    }

    public IEnumerable<Perfil> ObterPerfil(Guid perfilId)
    {
      return Db.Perfil.Where(p => p.Id.Equals(perfilId));
    }
  }
}
