using System.Threading.Tasks;
using Proj4Me.Domain.Core.Commands;
using Proj4Me.Domain.Core.Events;
using Proj4Me.Domain.Interfaces;
using MediatR;

namespace Proj4Me.Domain.Handlers
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventStore _eventStore;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
           // _eventStore = eventStore;
        }

        public async Task EnviarComando<T>(T comando) where T : Command
        {
            await _mediator.Send(comando);
        }

        public async Task PublicarEvento<T>(T evento) where T : Event
        {
            //if (!evento.MessageType.Equals("DomainNotification"))
            //    _eventStore?.SalvarEvento(evento);

            await _mediator.Publish(evento);
        }
    }
}