using finalproject.Application.Users.Commands;
using finalproject.Domain.Entities;
using finalproject.Domain.Interface;
using MediatR;

namespace finalproject.Application.Users.Handlers
{
    public class DeactivateUserHandler : IRequestHandler<DeactivateUserCommand, bool>
    {
        private readonly IUserService _userRepository;
        public DeactivateUserHandler(IUserService userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
        {
            User user = await _userRepository.GetUserByIdAsync(request.id);
            if (user.status == false)
            {
                return false;
            }
            user.status = false;
            await _userRepository.UpdateUserStatusAsync(user);
            return true;
        }
    }
}
