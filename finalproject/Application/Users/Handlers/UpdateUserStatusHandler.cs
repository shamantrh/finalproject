using finalproject.Application.Users.Commands;
using finalproject.Domain.Entities;
using finalproject.Domain.Interface;
using MediatR;

namespace finalproject.Application.Users.Handlers
{
    public class UpdateUserStatusHandler : IRequestHandler<UpdateUserStatusCommand, bool>
    {
        private readonly IUserService userService;
        public UpdateUserStatusHandler(IUserService _userService)
        {
            userService = _userService;
        }

        public async Task<bool> Handle(UpdateUserStatusCommand request, CancellationToken cancellationToken) {
            User user = await userService.GetUserById(request.id);
            if (user == null)
            {
                return false;
            }
            user.status=!user.status;
            await userService.UpdateUserStatusAsync(user);
            return true;
        }
    }
}
