using Proj4Me.Application.ViewModels;
using System;
using System.Collections.Generic;


namespace Proj4Me.Application.Interfaces
{
  public interface IColaboradorAppService : IDisposable
  {
    void Register(ColaboradorViewModel eventoViewModel);
    IEnumerable<ColaboradorViewModel> GetAll();
    IEnumerable<ColaboradorViewModel> GetProjetoByColaborador(Guid colaboradorId);
    ColaboradorViewModel GetColaboradorById(Guid id);
    void Update(ColaboradorViewModel projetoAreaServicoViewModel);
    void Remove(Guid id);

    //public void Dispose()
    //{
    //  throw new NotImplementedException();
    //}
  }
}
