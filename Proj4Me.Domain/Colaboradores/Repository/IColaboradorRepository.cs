using Proj4Me.Domain.Interfaces;
using System;
using System.Collections.Generic;
namespace Proj4Me.Domain.Colaboradores.Repository
{
  public interface IColaboradorRepository : IRepository<Colaborador>
  {
    IEnumerable<Colaborador> ObterColaborador(Guid colaboradorId);
  }
}