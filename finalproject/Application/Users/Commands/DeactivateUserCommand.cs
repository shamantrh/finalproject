using MediatR;

namespace finalproject.Application.Users.Commands 
{
    public class DeactivateUserCommand : IRequest<bool>
    {
        public int id { get; set; }
        public DeactivateUserCommand(int _id)
        {
            id = _id;
        }
    }
}
