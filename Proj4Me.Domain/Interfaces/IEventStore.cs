using Proj4Me.Domain.Core.Events;

namespace Proj4Me.Domain.Interfaces
{
    public interface IEventStore
    {
        void SalvarEvento<T>(T evento) where T : Event;
    }
}