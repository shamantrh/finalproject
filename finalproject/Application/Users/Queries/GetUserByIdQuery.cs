using finalproject.Domain.Entities;
using MediatR;

namespace finalproject.Application.Users.Queries
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public int id { get; set; }
        public GetUserByIdQuery(int _id)
        {
            id = _id;
        }
    }
}
