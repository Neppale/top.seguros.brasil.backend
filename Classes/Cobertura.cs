using Dapper;
using Microsoft.Data.SqlClient;

public class Cobertura
{
  public int id_cobertura { get; set; }
  public string nome { get; set; }
  public string descricao { get; set; }
  public string valor { get; set; }
  public bool status { get; set; }

  public Cobertura(string name, string description, double price, bool status)
  {
    this.nome = name;
    this.descricao = description;
    this.valor = price.ToString();
    this.status = status;
  }

  public Cobertura()
  {
  }

  /** <summary> Esta função retorna todas as coberturas no banco de dados. </summary>**/

  public IEnumerable<Cobertura> Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    var data = connectionString.Query<Cobertura>("SELECT * from Coberturas");

    Console.WriteLine("[INFO] A request for all 'cobertura' was made. The response is not a mock. :)");

    return data;
  }

  /** <summary> Esta função retorna uma cobertura específica no banco de dados. </summary>**/
  public IEnumerable<Cobertura> Get(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.Query<Cobertura>($"SELECT * from Coberturas WHERE id_cobertura={id}");

    Console.WriteLine("[INFO] A request for a single 'cobertura' was made. The response is not a mock. :)");

    if (data.Count() == 0) throw new BadHttpRequestException("Cobertura não encontrada.", statusCode: 404);

    return data;
  }

  /** <summary> Esta função insere uma cobertura no banco de dados. </summary>**/
  public IResult Insert(Cobertura cobertura, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    Console.WriteLine("[INFO] A request to post to 'Coberturas' was made :)");

    try
    {
      // Verificando se alguma das propriedades do Cobertura é nulo.
      bool NullProperty = cobertura.GetType().GetProperties()
                              .All(p => p.GetValue(cobertura) != null);
      if (!NullProperty) return Results.BadRequest("Há um campo inválido na sua requisição.");

      var data = connectionString.Query<Cobertura>($"INSERT INTO Coberturas (nome, descricao, valor, status) VALUES ('{cobertura.nome}', '{cobertura.descricao}', '{cobertura.valor}', '{cobertura.status}')");

      return Results.StatusCode(201);
    }
    catch (BadHttpRequestException)
    {
      //TODO: Exception Handler para mostrar o erro/statusCode correto com base na mensagem enviada pelo SQL server.
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}