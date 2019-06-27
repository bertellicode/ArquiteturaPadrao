using System.Collections.Generic;
using System.Threading.Tasks;
using ArquiteturaPadrao.Domain.Core.Commands;
using ArquiteturaPadrao.Domain.Core.Events;
using ArquiteturaPadrao.Domain.Core.Queries;

namespace ArquiteturaPadrao.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task<TReturn> SendQuery<TReturn>(QuerySingle<TReturn> querySingle) where TReturn : class;
        Task<IEnumerable<TReturn>> SendQueryCollection<TReturn>(QueryCollection<TReturn> queryCollection) where TReturn : class;
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
