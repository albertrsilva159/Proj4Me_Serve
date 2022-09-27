using Proj4Me.Domain.Core.Commands;
using System;

namespace Proj4Me.Domain.Interfaces
{
  public interface IUnitOfWork : IDisposable
  {
    bool Commit();

  }
}
