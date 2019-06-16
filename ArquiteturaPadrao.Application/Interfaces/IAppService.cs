using System;

namespace ArquiteturaPadrao.Application.Interfaces
{
    public interface IAppService : IDisposable
    {
        bool Commit();
    }
}
