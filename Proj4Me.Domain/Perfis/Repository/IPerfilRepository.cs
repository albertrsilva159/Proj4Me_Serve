using Proj4Me.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace Proj4Me.Domain.Perfis.Repository
{
  public interface IPerfilRepository : IRepository<Perfil>
  {
    IEnumerable<Perfil> ObterPerfil(Guid perfilId);
  }
}