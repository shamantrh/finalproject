using finalproject.Application.Users.Queries;
using finalproject.Domain.DTOs;
using finalproject.Domain.Entities;
using finalproject.Domain.Interface;
using MediatR;

namespace finalproject.Application.Users.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly IUserService _userService;
        public GetUserByIdQueryHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<User> Handle(GetUserByIdQuery getUserQuery, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByIdAsync(getUserQuery.id);
            if (user == null) return null;
            return user;
        }

    }
}
