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
  public IResult Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.Query<Terceirizado>("SELECT * from Terceirizados WHERE status='true'");

    return Results.Ok(data);
  }
  /** <summary> Esta função retorna um terceirizado específico no banco de dados. </summary>**/
  public IResult Get(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.QueryFirstOrDefault<Terceirizado>("SELECT * from Terceirizados WHERE id_terceirizado=@Id", new { Id = id });
    if (data == null) return Results.NotFound("Terceirizado não encontrado.");

    return Results.Ok(data);
  }
  /** <summary> Esta função insere uma Terceirizado no banco de dados. </summary>**/
  public IResult Insert(Terceirizado terceirizado, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    try
    {
      // Verificando se alguma das propriedades do Terceirizado é nula ou vazia.
      bool hasValidProperties = NullPropertyValidator.Validate(terceirizado);
      if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

      // Validando CNPJ
      bool cnpjIsValid = CnpjValidator.Validate(terceirizado.cnpj);
      if (!cnpjIsValid) return Results.BadRequest("O CNPJ informado é inválido.");

      var data = connectionString.Query<Terceirizado>("INSERT INTO Terceirizados (nome, funcao, cnpj, telefone, valor, status) VALUES (@Nome, @Funcao, @Cnpj, @Telefone, @Valor, @Status)", new { Nome = terceirizado.nome, Funcao = terceirizado.funcao, Cnpj = terceirizado.cnpj, Telefone = terceirizado.telefone, Valor = terceirizado.valor, Status = terceirizado.status });

      return Results.StatusCode(201);
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
  /** <summary> Esta função altera um terceirizado no banco de dados. </summary>**/
  public IResult Update(int id, Terceirizado terceirizado, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se alguma das propriedades do terceirizado é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(terceirizado);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    bool cnpjIsValid = CnpjValidator.Validate(terceirizado.cnpj);
    if (!cnpjIsValid) return Results.BadRequest("O CNPJ informado é inválido.");

    try
    {
      connectionString.Query<Terceirizado>("UPDATE Terceirizados SET nome = @Nome, funcao = @Funcao, cnpj = @Cnpj, telefone = @Telefone, valor = @Valor, status = @Status WHERE id_terceirizado = @Id", new { Nome = terceirizado.nome, Funcao = terceirizado.funcao, Cnpj = terceirizado.cnpj, Telefone = terceirizado.telefone, Valor = terceirizado.valor, Status = terceirizado.status, Id = id });
      return Results.Ok();
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}