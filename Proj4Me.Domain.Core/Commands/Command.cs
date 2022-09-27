using System;
using MediatR;
using Proj4Me.Domain.Core.Events;

namespace Proj4Me.Domain.Core.Commands
{
  //o comando é um evento, porem do tipo command
  public class Command : Message, IRequest
  {
    public DateTime Timestamp { get; private set; }

    public Command()
    {
      Timestamp = DateTime.Now;
    }
  }
}
