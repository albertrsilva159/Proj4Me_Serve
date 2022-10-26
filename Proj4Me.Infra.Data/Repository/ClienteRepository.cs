using Proj4Me.Domain.Clientes;
using Proj4Me.Domain.Clientes.Repository;
using Proj4Me.Domain.Colaboradores.Repository;
using Proj4Me.Domain.Colaboradores;
using Proj4Me.Infra.Data.Context;
using Proj4Me.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Proj4Me.Infra.Data.Repository
{
  public class ClienteRepository : Repository<Cliente>, IClienteRepository
  {
    public ClienteRepository(ProjetoAreaServicoContext context) : base(context)
    { }

    public IEnumerable<Cliente> ObterCliente(Guid clienteId)
    {
      return Db.Cliente.Where(p => p.ClienteId.Equals(clienteId));
    }
  }
}