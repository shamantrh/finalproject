using finalproject.Domain.Enums;

namespace finalproject.Domain.Entities
{
    public class User:IDisposable
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public Role role { get; set; }
        public bool status { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
