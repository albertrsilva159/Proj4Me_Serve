using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proj4Me.Domain.Core.Events;

namespace Proj4Me.Domain.Core.Commands
{
  //o comando é um evento, porem do tipo command
  public class Command : Message
  {
    public DateTime Timestamp { get; private set; }

    public Command()
    {
      Timestamp = DateTime.Now;
    }
  }
}
