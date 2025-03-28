using finalproject.Application.Users.Commands;
using finalproject.Domain.Interface;
using MediatR;

namespace finalproject.Application.Users.Handlers
{
    public class DeleteUserHandler: IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserService userService;
        public DeleteUserHandler(IUserService _userService)
        {
            userService = _userService;
        }
        public async Task<bool> Handle(DeleteUserCommand deleteUserCommand, CancellationToken cancellationToken)
        {
            var user = await userService.GetUserByIdAsync(deleteUserCommand.id);
            if (user == null)
            {
                return false;
            }

            await userService.DeleteUserAsync(user);
            return true;
            
        }
    }
}
