using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj4Me.Domain.Core.Events
{
  /// <summary>
  /// Evento da aplicação sao tudo aquilo que é disparado, mas no fundo todo evento é uma mensagem
  /// </summary>
  public class Message
  {
    //mostra o tipo de mensagem
    public string MessageType { get; protected set; }
    // identificar o agregado que esta disparando a mensagem
    public Guid AggregateId { get; protected set; }


    protected Message()
    {
      MessageType = GetType().Name; // nome da classe porque vai mostrar de qual classe esta herdando
    }
  }
}
