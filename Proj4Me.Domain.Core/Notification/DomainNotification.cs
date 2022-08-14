using Proj4Me.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj4Me.Domain.Core.Notification
{
  public class DomainNotification : Event
  {
    public Guid DomainNotificationId { get; private set; }
    public string Key { get; private set; } // nome evento
    public string Value { get; private set; } // mensagem
    public int Version { get; private set; } // versao de notificacao

    public DomainNotification(string key, string value)
    {
      DomainNotificationId = Guid.NewGuid();
      Key = key;
      Value = value;
      Version = 1;
    }
  }
}
