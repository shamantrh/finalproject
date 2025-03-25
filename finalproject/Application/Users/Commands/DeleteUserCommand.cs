using MediatR;

namespace finalproject.Application.Users.Commands
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public int id { get; set; }
        public DeleteUserCommand(int _id)
        {
            id = _id;
        }
    }
}
