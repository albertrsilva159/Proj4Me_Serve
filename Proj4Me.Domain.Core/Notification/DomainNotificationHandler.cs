﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj4Me.Domain.Core.Notification
{
  public class DomainNotificationHandler : IDomainNotificationHandler<DomainNotification>
  {
    private List<DomainNotification> _notifications;

    public DomainNotificationHandler()
    {
      _notifications = new List<DomainNotification>();
    }
    public List<DomainNotification> GetNotifications()
    {
      return _notifications;
    }

    public void Handle(DomainNotification message)
    {
      _notifications.Add(message);
      Console.ForegroundColor = ConsoleColor.Red;
      Console.WriteLine($"Erro: {message.Key} - {message.Value}");
    }

    public bool HasNotifications()
    {
      return _notifications.Any();
    }

    public void Dispose()
    {
      _notifications = new List<DomainNotification>();
    }
  }
}
