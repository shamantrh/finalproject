using MediatR;

namespace finalproject.Application.Users.Commands
{
    public class UpdateUserStatusCommand : IRequest<bool>
    {
        public int id { get; set; }
        public UpdateUserStatusCommand(int _id)
        {
            id = _id;
        }
    }
}
