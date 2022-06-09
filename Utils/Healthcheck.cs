using Dapper;
using Microsoft.Data.SqlClient;

namespace tsb.mininal.policy.engine.Utils
{
  public abstract class HealthCheck
  {
    public static IResult Check(string dbConnectionString)
    {
            int count = 0;
            SqlConnection connectionString = new SqlConnection(dbConnectionString);
            
            connectionString.Query($"SELECT * from Clientes");
            count++;
            connectionString.Query($"SELECT * from Coberturas");
            count++;
            connectionString.Query($"SELECT * from Usuarios");
            count++;
            connectionString.Query($"SELECT * from Veiculos");
            count++;
            connectionString.Query($"SELECT * from Terceirizados");
            count++;
            connectionString.Query($"SELECT * from Apolices");
            count++;

            return Results.Accepted();
        }
    public static string FormatDate()
    {
      return "1970-01-01";
    }
  }
}
