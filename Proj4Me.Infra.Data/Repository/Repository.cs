using Proj4Me.Domain.Core.Models;
using Proj4Me.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Proj4Me.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Proj4Me.Infra.Data.Repository
{
  public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity<TEntity>
  {
    protected ProjetoAreaServicoContext Db;
    protected DbSet<TEntity> DbSet;

    protected Repository(ProjetoAreaServicoContext context)
    {
      Db = context;
      DbSet = Db.Set<TEntity>();
    }

    public virtual void Add(TEntity obj)
    {
      DbSet.Add(obj);
    }

    public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
      return DbSet.AsNoTracking().Where(predicate);
    }

    public virtual IEnumerable<TEntity> GetAll()
    {
      return DbSet.ToList();
    }

    public virtual TEntity GetById(Guid id)
    {
      return DbSet.AsNoTracking().FirstOrDefault(t => t.Id == id);
    }

    public virtual void Remover(Guid id)
    {
      DbSet.Remove(DbSet.Find(id));
    }

    public int SaveChanges()
    {
      return Db.SaveChanges();
    }

    public virtual void Update(TEntity obj)
    {
      DbSet.Update(obj);
    }
    /// <summary>
    /// Dispose para acabar com a instancia do contexto
    /// </summary>
    public virtual void Dispose()
    {
      Db.Dispose();
    }
  }
}
