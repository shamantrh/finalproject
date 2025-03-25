using System.Data;
using Microsoft.Data.SqlClient;

namespace finalproject.Infrastructure.Persistence
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection() {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
