using AutoMapper;

namespace ArquiteturaPadrao.Domain.Core.Queries.QueryHandlers
{
    public class QueryHandler
    {
        protected readonly IMapper _mapper;

        public QueryHandler(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
