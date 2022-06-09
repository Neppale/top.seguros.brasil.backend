using Dapper;
using Microsoft.Data.SqlClient;
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
  public IEnumerable<Veiculo> Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.Query<Veiculo>("SELECT * from Veiculos");

    Console.WriteLine("[INFO] A request for all 'Veiculos' was made. The response is not a mock. :)");

    return data;
  }
  /** <summary> Esta função retorna um veículo específico no banco de dados. </summary>**/
  public IEnumerable<Veiculo> Get(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.Query<Veiculo>($"SELECT * from Veiculos WHERE id_Veiculo={id}");

    Console.WriteLine("[INFO] A request for a single 'Veiculo' was made. The response is not a mock. :)");
    if (data.Count() == 0) throw new BadHttpRequestException("Veiculo não encontrado.", statusCode: 404);

    return data;
  }

  /** <summary> Esta função insere um Veiculo no banco de dados. </summary>**/
  public IResult Insert(Veiculo veiculo, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    Console.WriteLine("[INFO] A request to post to 'Veiculos' was made :)");

    try
    {
      // Verificando se alguma das propriedades do Veiculo é nulo.
      bool NullProperty = veiculo.GetType().GetProperties()
                              .All(p => p.GetValue(veiculo) != null);
      if (!NullProperty) return Results.BadRequest("Há um campo inválido na sua requisição.");

      var data = connectionString.Query<Veiculo>($"INSERT INTO Veiculos (marca, modelo, ano, uso, placa, renavam, sinistrado, id_cliente) VALUES ('{veiculo.marca}', '{veiculo.modelo}', '{veiculo.ano}', '{veiculo.uso}', '{veiculo.placa}', '{veiculo.renavam}', '{veiculo.sinistrado}', '{veiculo.id_cliente}')");

      return Results.StatusCode(201);
    }
    catch (BadHttpRequestException)
    {
      //TODO: Exception Handler para mostrar o erro/statusCode correto com base na mensagem enviada pelo SQL server.
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}