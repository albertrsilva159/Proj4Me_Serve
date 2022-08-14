using Proj4Me.Domain.Core.Commands;
using Proj4Me.Domain.Interfaces;
using Proj4Me.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj4Me.Infra.Data.UoW
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly ProjetoAreaServicoContext _context;

    public UnitOfWork(ProjetoAreaServicoContext context)
    {
      _context = context;
    }

    public CommandResponse Commit()
    {
      //Savechange retorna o numero de linhas afetadas
      var rowsAffected = _context.SaveChanges();
      return new CommandResponse(rowsAffected > 0);
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}