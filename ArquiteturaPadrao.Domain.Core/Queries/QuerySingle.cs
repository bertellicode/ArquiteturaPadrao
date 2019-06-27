using System;
using MediatR;

namespace ArquiteturaPadrao.Domain.Core.Queries
{
    public class QuerySingle<TEntity> : IRequest<TEntity> where TEntity : class
    {
        public Guid Id { get; set; }
    }
}
