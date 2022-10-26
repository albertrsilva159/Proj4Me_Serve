using Proj4Me.Domain.Interfaces;
using System;
using System.Collections.Generic;
namespace Proj4Me.Domain.Clientes.Repository
{
  public interface IClienteRepository : IRepository<Cliente>
  {
    IEnumerable<Cliente> ObterCliente(Guid colaboradorId);
  }
}