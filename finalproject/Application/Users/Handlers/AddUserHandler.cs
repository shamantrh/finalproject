using finalproject.Application.Users.Commands;
using finalproject.Domain.Entities;
using finalproject.Domain.Interface;
using MediatR;

namespace finalproject.Application.Users.Handlers
{
    public class AddUserHandler : IRequestHandler<AddUserCommand, int>
    {
        private readonly IUserService _userService;
        public AddUserHandler(IUserService userService) { 
            _userService = userService;
        }
        public async Task<int> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            User user = new User
            {
                name = request.name,
                email = request.email,
                role = request.role,
                status = request.status
            };
            return await _userService.AddUserAsync(user);

        }
    }
}
