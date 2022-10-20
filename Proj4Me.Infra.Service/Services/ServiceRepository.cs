using Microsoft.Extensions.Configuration;
using Proj4Me.Infra.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http.Headers;
using Proj4Me.Infra.Service.Model;

namespace Proj4Me.Infra.Service.Services
{
  public class ServiceRepository : IServiceRepository
  {

    private readonly IConfiguration _configuration;
    private readonly string _URLPROJ4MEAUTH;
    private readonly string _URLPROJ4ME;
    private readonly string _token;
    public ServiceRepository(IConfiguration configuration)
    {
      _configuration = configuration;
      _URLPROJ4MEAUTH = _configuration["CredenciaisProj4Me:URLProj4MeAuth"];
      _URLPROJ4ME = _configuration["CredenciaisProj4Me:URLProj4Me"];
      _token = GerarToken();
    }

    //public string GerarToken()
    //{
    //  var testeee = _configuration["CredenciaisProj4Me:usuario"];

    //  return String.Empty;
    //}
    public string GerarToken()
    {
      using (HttpClient httpClient = new HttpClient())
      {
        Dictionary<string, string> tokenDetails = null;
        HttpClient client = new HttpClient();

        client.BaseAddress = new Uri(_URLPROJ4MEAUTH);
        var conta = new
        {
          username = _configuration["CredenciaisProj4Me:usuario"],
          password = _configuration["CredenciaisProj4Me:senha"]
        };
        string jsonData = JsonConvert.SerializeObject(conta);
        var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
        var result = client.PostAsync("auth/login/v2", content).Result;

        if (result.IsSuccessStatusCode)
        {
          var token = JsonConvert.DeserializeObject<Dictionary<string, string>>(result.Content.ReadAsStringAsync().Result);

          return token.FirstOrDefault().Value;
        }
        return result.RequestMessage.ToString();
      }
    }

    public List<ProjetoProj4Me> ListarProjetos()
    {
      List<ProjetoProj4Me> listaPorProjetos = new List<ProjetoProj4Me>();
      using (HttpClient client = new HttpClient())
      {
        //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _token);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        var req = new HttpRequestMessage(HttpMethod.Get, _URLPROJ4ME);

        HttpResponseMessage resp = client.SendAsync(req).Result;
        var result = resp.Content.ReadAsStringAsync();

        listaPorProjetos = JsonConvert.DeserializeObject<List<ProjetoProj4Me>>(result.Result) as List<ProjetoProj4Me>;
        //var testee = convert.Where(x => x.index < 3);
      }
      return listaPorProjetos;
    }

    public ProjetoProj4Me ProjetoPorIndex(long indexProjeto)
    {
      ProjetoProj4Me Projeto = new ProjetoProj4Me();
      using (HttpClient client = new HttpClient())
      {
        //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _token);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        var req = new HttpRequestMessage(HttpMethod.Get, _URLPROJ4ME + indexProjeto);

        HttpResponseMessage resp = client.SendAsync(req).Result;
        var result = resp.Content.ReadAsStringAsync();

        Projeto = JsonConvert.DeserializeObject<ProjetoProj4Me>(result.Result) ;
        //var testee = convert.Where(x => x.index < 3);
      }
      return Projeto;
    }

    public List<TarefaProj4Me> ListarTasksProjeto(long codProjeto)
    {
      List<TarefaProj4Me> listaTasksPorProjeto = new List<TarefaProj4Me>();
      using (HttpClient client = new HttpClient())
      {
        /// HttpClient client = new HttpClient();
        //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _token);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        var req = new HttpRequestMessage(HttpMethod.Get, _URLPROJ4ME + codProjeto.ToString() + "/tasks");
        //req.Headers.Add("projects", codProjeto.ToString() + "/tasks");


        HttpResponseMessage resp = client.SendAsync(req).Result;
        var result = resp.Content.ReadAsStringAsync();

        listaTasksPorProjeto = JsonConvert.DeserializeObject<List<TarefaProj4Me>>(result.Result) as List<TarefaProj4Me>;
        //var testee = convert.Where(x => x.index < 3);
      }
      return listaTasksPorProjeto;
    }

    public List<TarefaEsforcoProj4Me> BuscarEsforcoEComentarioTasksProjeto(long codProjeto, long codtask)
    {
      List<TarefaEsforcoProj4Me> tarefaEsforco = new List<TarefaEsforcoProj4Me>();

      using (HttpClient client = new HttpClient())
      {
        /// HttpClient client = new HttpClient();
        //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _token);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        var req = new HttpRequestMessage(HttpMethod.Get, _URLPROJ4ME + codProjeto.ToString() + "/tasks/" + codtask.ToString() + "/efforts-rec");
        //req.Headers.Add("projects", codProjeto.ToString() + "/tasks");


        HttpResponseMessage resp = client.SendAsync(req).Result;
        var result = resp.Content.ReadAsStringAsync();

        tarefaEsforco = JsonConvert.DeserializeObject<List<TarefaEsforcoProj4Me>>(result.Result) as List<TarefaEsforcoProj4Me>;
        //var testee = convert.Where(x => x.index < 3);
      }
      return tarefaEsforco;
    }


  }
}
