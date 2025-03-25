using MediatR;
using finalproject.Domain.Entities;
using finalproject.Domain.DTOs;
namespace finalproject.Application.Users.Queries
{
    public class GetUserQuery : IRequest<GetUser>
    {
        public string _searchTerm { get; set; }
        public int _pageNumber { get; set; }
        public int _pageSize { get; set; }
        public GetUserQuery(string searchTerm, int pageNumber, int pageSize) {
            _searchTerm = searchTerm;
            _pageNumber = pageNumber;
            _pageSize = pageSize;
        }


    }
}
