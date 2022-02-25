using API.Mediatr.Person.UIModels;
using MediatR;

namespace API.Mediatr.Person
{
    public class QueryById:IRequest<PersonDto>
    {
        public QueryById(int personId)
        {
            PersonId = personId;
        }

        public int PersonId { get; }
    }

    public class QueryByIdHandler : IRequestHandler<QueryById, PersonDto>
    {
        public Task<PersonDto> Handle(QueryById request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }


}
