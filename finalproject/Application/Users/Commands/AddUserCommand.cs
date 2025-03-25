using finalproject.Domain.Enums;
using MediatR;

namespace finalproject.Application.Users.Commands
{
    public class AddUserCommand : IRequest<int>
    {
        public string name { get; set; }
        public string email { get; set; }
        public Role role { get; set; }
        public bool status { get; set; }
    }
}
