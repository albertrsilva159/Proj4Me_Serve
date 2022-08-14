using Proj4Me.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj4Me.Application.Interfaces
{
  public interface IProjetoAreaServicoAppService : IDisposable
  {
    void Register(ProjetoAreaServicoViewModel eventoViewModel);
    IEnumerable<ProjetoAreaServicoViewModel> GetAll();
    IEnumerable<ProjetoAreaServicoViewModel> GetProjetoByColaborador(Guid colaboradorId);
    ProjetoAreaServicoViewModel GetProjetoById(Guid id);
    void Update(ProjetoAreaServicoViewModel projetoAreaServicoViewModel);
    void Remove(Guid id);
    ProjetoAreaServicoViewModel GetProjetoColaboradorPerfil(Guid id);

    //public void Dispose()
    //{
    //  throw new NotImplementedException();
    //}
  }
}
