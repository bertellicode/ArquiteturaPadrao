using System;
using System.Collections.Generic;
using MediatR;

namespace ArquiteturaPadrao.Domain.Core.Queries
{
    public class QueryCollection<TEntity>: IRequest<IEnumerable<TEntity>> where TEntity : class
    {
        public Guid Id { get; set; }
    }
}
