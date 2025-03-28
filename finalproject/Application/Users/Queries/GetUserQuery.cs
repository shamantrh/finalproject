using MediatR;
using finalproject.Domain.Entities;
using finalproject.Domain.DTOs;
using finalproject.Domain.Enums;
namespace finalproject.Application.Users.Queries
{
    public class GetUserQuery : IRequest<GetUser>
    {
        public string _searchTerm { get; set; }
        public int _pageNumber { get; set; }
        public int _pageSize { get; set; }
        public Role? _role { get; set; }
        public bool? _status { get; set; }

        public GetUserQuery(string searchTerm, Role? role, bool? status, int pageNumber, int pageSize) {
            _searchTerm = searchTerm;
            _pageNumber = pageNumber;
            _pageSize = pageSize;
            _role = role;
            _status = status;

        }


    }
}
