using Dapper;
using Microsoft.Data.SqlClient;
using tsb.mininal.policy.engine.Utils;

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

    var data = connectionString.Query<Cobertura>("SELECT * from Coberturas WHERE status='true'");

    return data;
  }

  /** <summary> Esta função retorna uma cobertura específica no banco de dados. </summary>**/
  public IEnumerable<Cobertura> Get(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.Query<Cobertura>($"SELECT * from Coberturas WHERE id_cobertura={id}");

    if (data.Count() == 0) throw new BadHttpRequestException("Cobertura não encontrada.", statusCode: 404);

    return data;
  }

  /** <summary> Esta função insere uma cobertura no banco de dados. </summary>**/
  public IResult Insert(Cobertura cobertura, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    try
    {
      // Verificando se alguma das propriedades do Cobertura é nula.
      bool isValid = NullPropertyValidator.Validate(cobertura);
      if (!isValid) return Results.BadRequest("Há um campo inválido na sua requisição.");

      var data = connectionString.Query<Cobertura>($"INSERT INTO Coberturas (nome, descricao, valor, status) VALUES ('{cobertura.nome}', '{cobertura.descricao}', '{cobertura.valor}', '{cobertura.status}')");

      return Results.StatusCode(201);
    }
    catch (BadHttpRequestException)
    {
      //TODO: Exception Handler para mostrar o erro/statusCode correto com base na mensagem enviada pelo SQL server.
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
  /** <summary> Esta função altera uma cobertura no banco de dados. </summary>**/
  public IResult Update(int id, Cobertura cobertura, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se alguma das propriedades do cobertura é nula.
    bool isValid = NullPropertyValidator.Validate(cobertura);
    if (!isValid) return Results.BadRequest("Há um campo inválido na sua requisição.");

    try
    {

      connectionString.Query<Cobertura>($"UPDATE Coberturas SET nome = '{cobertura.nome}', descricao = '{cobertura.descricao}', valor = '{cobertura.valor}', status = '{cobertura.status}' WHERE id_cobertura = {id}");
      return Results.Ok();
    }
    catch (BadHttpRequestException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}