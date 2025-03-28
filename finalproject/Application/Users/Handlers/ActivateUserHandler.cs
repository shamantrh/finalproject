using finalproject.Application.Users.Commands;
using MediatR;
using finalproject.Domain.Interface;
using finalproject.Domain.Entities;

namespace finalproject.Application.Users.Handlers
{
    public class ActivateUserHandler : IRequestHandler<ActivateUserCommand, bool>
    {
        private readonly IUserService _userRepository;
        public ActivateUserHandler(IUserService userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetUserByIdAsync(request.id);
            if (user.status == true)
            {
                return false;
            }
            user.status = true;
            await _userRepository.UpdateUserStatusAsync(user);
            return true;
        }
    }
}
