using Dapper;
using Microsoft.Data.SqlClient;
using tsb.mininal.policy.engine.Utils;
public class Veiculo
{
  public int id_veiculo { get; set; }
  public string marca { get; set; }
  public string modelo { get; set; }
  public int ano { get; set; }
  public string uso { get; set; }
  public string placa { get; set; }
  public string renavam { get; set; }
  public bool sinistrado { get; set; }
  public int id_cliente { get; set; }

  public Veiculo()
  {
  }

  public Veiculo(string brand, string model, int year, string usage, string plate, string renavam, bool sinistrado, int idcliente)
  {
    this.marca = brand;
    this.modelo = model;
    this.ano = year;
    this.uso = usage;
    this.placa = plate;
    this.renavam = renavam;
    this.sinistrado = sinistrado;
    this.id_cliente = idcliente;
  }

  /** <summary> Esta função retorna todos os veículos no banco de dados. </summary>**/
  public IResult Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.Query<Veiculo>("SELECT * from Veiculos");

    return Results.Ok(data);
  }
  /** <summary> Esta função retorna um veículo específico no banco de dados. </summary>**/
  public IResult Get(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.QueryFirstOrDefault<Veiculo>("SELECT * from Veiculos WHERE id_Veiculo = @Id", new { Id = id });

    if (data == null) return Results.NotFound("Veículo não encontrado.");

    return Results.Ok(data);
  }

  /** <summary> Esta função insere um Veiculo no banco de dados. </summary>**/
  public IResult Insert(Veiculo veiculo, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    try
    {
      // Verificando se alguma das propriedades do Veiculo é nula ou vazia.
      bool hasValidProperties = NullPropertyValidator.Validate(veiculo);
      if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

      bool RenavamIsValid = RenavamValidator.Validate(veiculo.renavam);
      if (!RenavamIsValid) return Results.BadRequest("O RENAVAM informado é inválido.");

      var data = connectionString.Query<Veiculo>("INSERT INTO Veiculos (marca, modelo, ano, uso, placa, renavam, sinistrado, id_cliente) VALUES (@Marca, @Modelo, @Ano, @Uso, @Placa, @Renavam, @Sinistrado, @IdCliente)", new { Marca = veiculo.marca, Modelo = veiculo.modelo, Ano = veiculo.ano, Uso = veiculo.uso, Placa = veiculo.placa, Renavam = veiculo.renavam, Sinistrado = veiculo.sinistrado, IdCliente = veiculo.id_cliente });

      return Results.StatusCode(201);
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }

  /** <summary> Esta função altera um Veículo no banco de dados. </summary>**/
  public IResult Update(int id, Veiculo veiculo, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se alguma das propriedades do veiculo é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(veiculo);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    bool RenavamIsValid = RenavamValidator.Validate(veiculo.renavam);
    if (!RenavamIsValid) return Results.BadRequest("O RENAVAM informado é inválido.");

    try
    {
      connectionString.Query("UPDATE Veiculos SET marca = @Marca, modelo = @Modelo, ano = @Ano, uso = @Uso, placa = @Placa, renavam = @Renavam, sinistrado = @Sinistrado, id_cliente = @IdCliente WHERE id_Veiculo = @Id", new { Marca = veiculo.marca, Modelo = veiculo.modelo, Ano = veiculo.ano, Uso = veiculo.uso, Placa = veiculo.placa, Renavam = veiculo.renavam, Sinistrado = veiculo.sinistrado, IdCliente = veiculo.id_cliente, Id = id });

      return Results.Ok();
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}