using System.Threading.Tasks;
using Proj4Me.Domain.Core.Commands;
using Proj4Me.Domain.Core.Events;

namespace Proj4Me.Domain.Interfaces
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task EnviarComando<T>(T comando) where T : Command;
    }
}