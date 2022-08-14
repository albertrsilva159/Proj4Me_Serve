using Proj4Me.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj4Me.Domain.Core.Notification
{
  public interface IDomainNotificationHandler<T> : IHandler<T> where T : Message
  {
    bool HasNotifications();
    List<T> GetNotifications();

  }
}
