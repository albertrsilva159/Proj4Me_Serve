using Proj4Me.Application.ViewModels;
using System;
using System.Collections.Generic;

namespace Proj4Me.Application.Interfaces
{
  public interface IPerfilAppService : IDisposable
  {
    void Register(PerfilViewModel perfilViewModel);
    IEnumerable<PerfilViewModel> GetAll();
    PerfilViewModel GetPerfilById(Guid id);
    void Update(PerfilViewModel perfilViewModel);
    void Remove(Guid id);

    //public void Dispose()
    //{
    //  throw new NotImplementedException();
    //}
  }
}
