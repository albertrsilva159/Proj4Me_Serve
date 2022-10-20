using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Proj4Me.Infra.Service.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Proj4Me.Infra.Service.Interfaces
{
  public interface IServiceRepository
  {
    public string GerarToken();
    public List<ProjetoProj4Me> ListarProjetos();
    public ProjetoProj4Me ProjetoPorIndex(long indexProjeto);
    public List<TarefaProj4Me> ListarTasksProjeto(long codProjeto);
    public List<TarefaEsforcoProj4Me> BuscarEsforcoEComentarioTasksProjeto(long codProjeto, long codtask);




  }
}
