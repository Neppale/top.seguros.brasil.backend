using Dapper;
using Microsoft.Data.SqlClient;

public class Cobertura
{
  public int id_cobertura { get; set; }
  public string nome { get; set; }
  public string descricao { get; set; }
  public double valor { get; set; }
  public bool status { get; set; }

  public Cobertura()
  {
    id_cobertura = 0;
    nome = "any_name";
    descricao = "any_description";
    valor = 0.00;
    status = true;
  }

  public Cobertura(string name, string description, double price, bool status)
  {
    this.nome = name;
    this.descricao = description;
    this.valor = price;
    this.status = status;
  }

  // Esta função retorna todas as coberturas.

  public IEnumerable<Cobertura> Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    var data = connectionString.Query<Cobertura>("SELECT * from Coberturas");

    Console.WriteLine("[INFO] A request for all 'cobertura' was made. The response is not a mock. :)");

    return data;
  }

  // Esta função retorna apenas uma cobertura em específico.
  public IEnumerable<Cobertura> Get(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.Query<Cobertura>($"SELECT * from Coberturas WHERE id_cobertura={id}");

    Console.WriteLine("[INFO] A request for a single 'cobertura' was made. The response is not a mock. :)");

    if (data.Count() == 0) throw new BadHttpRequestException("Cobertura não encontrada.", statusCode: 404);

    return data;
  }
}