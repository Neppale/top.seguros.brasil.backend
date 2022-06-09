using Dapper;
using Microsoft.Data.SqlClient;

namespace tsb.mininal.policy.engine.Utils
{
  public abstract class HealthCheck
  {
    public static IResult Check(string dbConnectionString)
    {
            SqlConnection connectionString = new SqlConnection(dbConnectionString);
           
            // Clean code? I don't know her.
            if (!connectionString.QuerySingle<bool>($"SELECT * from Clientes WHERE id_cliente = 1")) return Results.StatusCode(503);

            if (!connectionString.QuerySingle<bool>($"SELECT * from Coberturas  WHERE id_cobertura = 1")) return Results.StatusCode(503);

            if (!connectionString.QuerySingle<bool>($"SELECT * from Usuarios WHERE id_usuario = 1")) return Results.StatusCode(503);

            if (!connectionString.QuerySingle<bool>($"SELECT * from Veiculos WHERE id_veiculo = 1")) return Results.StatusCode(503);

            if (!connectionString.QuerySingle<bool>($"SELECT * from Terceirizados WHERE id_terceirizado = 1")) return Results.StatusCode(503);

            if (!connectionString.QuerySingle<bool>($"SELECT * from Apolices WHERE id_apolice = 1")) return Results.StatusCode(503);

            if (!connectionString.QuerySingle<bool>($"SELECT id_cobertura from Coberturas WHERE id_cobertura = 1")) return Results.StatusCode(503);



            return Results.Ok();
        }
  }
}
