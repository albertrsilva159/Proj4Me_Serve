using System;
using System.Collections.Generic;
using System.Text;

namespace Proj4Me.Infra.Data.Utils
{
  public static class TratamentoHorasEsforcoTarefa
  {
    public static string ConverteFormataHorasEsforco(double tempo)
    {      
      TimeSpan tempoTotalTarefa = TimeSpan.FromMinutes(tempo);
      string tempoTotalTarefaFormatado = string.Format("{0:D2}h:{1:D2}m", tempoTotalTarefa.Hours, tempoTotalTarefa.Minutes);

      return tempoTotalTarefaFormatado;
    }
  }
}
