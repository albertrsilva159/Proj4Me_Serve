using Proj4Me.Domain.Core;
using Proj4Me.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Proj4Me.Domain.Interfaces
{
  public interface IRepository<TEntity> : IDisposable where TEntity : Entity<TEntity>
  {
    void Add(TEntity obj);
    TEntity GetById(Guid id);
    IEnumerable<TEntity> GetAll();
    void Update(TEntity obj);
    void Remover(Guid id);
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    int SaveChanges();
  }
}
