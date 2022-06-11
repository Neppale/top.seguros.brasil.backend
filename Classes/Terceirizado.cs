using Dapper;
using Microsoft.Data.SqlClient;
using tsb.mininal.policy.engine.Utils;
public class Terceirizado
{
  public int id_terceirizado { get; set; }
  public string nome { get; set; }
  public string funcao { get; set; }
  public string cnpj { get; set; }
  public string telefone { get; set; }

  public double valor { get; set; }
  public bool status { get; set; }

  public Terceirizado(string fullName, string function, string cnpj, string phone, double price, bool status)
  {
    this.nome = fullName;
    this.funcao = function;
    this.cnpj = cnpj;
    this.telefone = phone;
    this.valor = price;
    this.status = status;
  }

  public Terceirizado()
  {
  }

  /** <summary> Esta função retorna todos os terceirizados no banco de dados. </summary>**/
  public IEnumerable<Terceirizado> Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.Query<Terceirizado>("SELECT * from Terceirizados");

    return data;
  }
  /** <summary> Esta função retorna um terceirizado específico no banco de dados. </summary>**/
  public IEnumerable<Terceirizado> Get(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.Query<Terceirizado>($"SELECT * from Terceirizados WHERE id_terceirizado={id}");
    if (data.Count() == 0) throw new BadHttpRequestException("Terceirizado não encontrado.", statusCode: 404);

    return data;
  }
  /** <summary> Esta função insere uma Terceirizado no banco de dados. </summary>**/
  public IResult Insert(Terceirizado terceirizado, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    try
    {
      // Verificando se alguma das propriedades do Terceirizado é nula.
      bool isValid = NullPropertyValidator.Validate(terceirizado);
      if (!isValid) return Results.BadRequest("Há um campo inválido na sua requisição.");

      // Validando CNPJ
      bool cnpjIsValid = CnpjValidator.Validate(terceirizado.cnpj);
      if (!cnpjIsValid) return Results.BadRequest("O CNPJ informado é inválido.");

      var data = connectionString.Query<Terceirizado>($"INSERT INTO Terceirizados (nome, funcao, cnpj, telefone, valor, status) VALUES ('{terceirizado.nome}', '{terceirizado.funcao}', '{terceirizado.cnpj}','{terceirizado.telefone}', '{terceirizado.valor}', '{terceirizado.status}')");

      return Results.StatusCode(201);
    }
    catch (BadHttpRequestException)
    {
      //TODO: Exception Handler para mostrar o erro/statusCode correto com base na mensagem enviada pelo SQL server.
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
  /** <summary> Esta função altera um terceirizado no banco de dados. </summary>**/
  public IResult Update(int id, Terceirizado terceirizado, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se alguma das propriedades do terceirizado é nula.
    bool isValid = NullPropertyValidator.Validate(terceirizado);
    if (!isValid) return Results.BadRequest("Há um campo inválido na sua requisição.");

    bool cnpjIsValid = CnpjValidator.Validate(terceirizado.cnpj);
    if (!cnpjIsValid) return Results.BadRequest("O CNPJ informado é inválido.");

    try
    {

      connectionString.Query<Terceirizado>($"UPDATE Terceirizados SET nome = '{terceirizado.nome}', funcao = '{terceirizado.funcao}', cnpj = '{terceirizado.cnpj}', valor = '{terceirizado.valor}', status = '{terceirizado.status}' WHERE id_terceirizado = {id}");
      return Results.Ok();
    }
    catch (BadHttpRequestException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}