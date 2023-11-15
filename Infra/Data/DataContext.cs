using Microsoft.Extensions.Configuration;
using MySqlConnector;
using static Dapper.SqlMapper;

namespace Infra.Data
{
    public class DataContext
    {
        public MySqlConnection connection { get; }
        public DataContext(IConfiguration configuration)
        {
            connection = new MySqlConnection(configuration.GetConnectionString("MySqlConnection"));
        }
    }
}