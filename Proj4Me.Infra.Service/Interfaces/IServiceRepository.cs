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
    public List<ProjetoProj4Me> ListarProjetoEspecifico(string indexProjeto);
    public List<TarefaProj4Me> ListarTasksProjeto(int codProjeto);
    public List<TarefaEsforcoProj4Me> BuscarEsforcoEComentarioTasksProjeto(int codProjeto, int codtask);




  }
}
