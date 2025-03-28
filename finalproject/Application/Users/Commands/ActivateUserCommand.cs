using MediatR;

namespace finalproject.Application.Users.Commands
{
    public class ActivateUserCommand : IRequest<bool>
    {
        public int id { get; set; }

        public ActivateUserCommand(int _id)
        {
            id=_id;
        }
    }
}
