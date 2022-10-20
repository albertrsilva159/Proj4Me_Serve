using System;

namespace Proj4Me.Services.Api.ViewModels
{
  public class TarefaViewModel
  {
    public TarefaViewModel(long index, string nomeTarefa, DateTime? dataEsforco, string nomeColaborador, string? comentario, string? tempoGastoDetalhado, string? totalTempoGasto)
    {
      Index = index;      
      NomeTarefa = nomeTarefa;
      DataEsforco = dataEsforco;
      NomeColaborador = nomeColaborador;
      Comentario = comentario;
      TempoGastoDetalhado = tempoGastoDetalhado;
      TotalTempoGasto = totalTempoGasto;
    }


    public TarefaViewModel() { }

    public long Index { get; set; }
    public string NomeTarefa { get; set; }
    public DateTime? DataEsforco { get; set; }
    public string NomeColaborador { get; set; }
    public string? Comentario { get; set; }
    public string? TempoGastoDetalhado { get; set; }
    public string? TotalTempoGasto { get; set; }
  }
}
