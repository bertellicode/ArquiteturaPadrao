using System.Threading.Tasks;
using ArquiteturaPadrao.Domain.Core.Commands;
using ArquiteturaPadrao.Domain.Core.Events;

namespace ArquiteturaPadrao.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
