using finalproject.Domain.Entities;

namespace finalproject.Domain.DTOs
{
    public class GetUser
    {
        public IEnumerable<User> users { get; set; }
        public int totalCount { get; set; }
    }
}
