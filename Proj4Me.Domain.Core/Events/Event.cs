using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj4Me.Domain.Core.Events
{
  public abstract class Event : Message, INotification
  {
    public DateTime Timestamp { get; set; }

    public Event()
    {
      Timestamp = DateTime.Now;
    }
  }
}
