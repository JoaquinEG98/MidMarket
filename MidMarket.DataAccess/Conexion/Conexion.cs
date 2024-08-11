using System.Configuration;
using System.Data.SqlClient;

namespace MidMarket.DataAccess.Conexion
{
    public class Conexion
    {
        private readonly string _server;
        private readonly string _base;
        public string conexion { get; set; }
        public Conexion()
        {
            _server = ConfigurationManager.AppSettings["server"];
            _base = ConfigurationManager.AppSettings["base"];

            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = _server,
                InitialCatalog = _base,
                IntegratedSecurity = true,
            };
            conexion = sqlConnectionStringBuilder.ConnectionString;
        }
    }
}
