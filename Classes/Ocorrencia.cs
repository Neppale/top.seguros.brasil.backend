using Dapper;
using Microsoft.Data.SqlClient;
using tsb.mininal.policy.engine.Utils;

public class Ocorrencia
{
  public int id_ocorrencia { get; set; }
  public string data { get; set; }
  public string local { get; set; }
  public string UF { get; set; }

  public string municipio { get; set; }
  public string descricao { get; set; }
  public string tipo { get; set; }
  public string? documento { get; set; } // Base64 file? Binary? We still don't know.
  public int id_veiculo { get; set; }
  public int id_cliente { get; set; }
  public int? id_terceirizado { get; set; }
  public string status { get; set; }

  public Ocorrencia(string date, string place, string UF, string city, string description, string type, string? document, int idveiculo, int idcliente, int? idterceirizado, string status)
  {

    this.data = date;
    this.local = place;
    this.UF = UF;
    this.municipio = city;
    this.descricao = description;
    this.tipo = type;
    this.documento = document;
    this.id_veiculo = idveiculo;
    this.id_cliente = idcliente;
    this.id_terceirizado = idterceirizado;
    this.status = status;
  }

  public Ocorrencia()
  {
  }

  /** <summary> Esta função retorna todas as ocorrências no banco de dados. </summary>**/

  public IResult Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    //TODO: Retornar documento das ocorrências.
    var data = connectionString.Query<Ocorrencia>("SELECT id_ocorrencia, data, local, UF, municipio, descricao, tipo, status, id_veiculo, id_cliente, id_terceirizado from Ocorrencias");

    return Results.Ok(data);
  }

  /** <summary> Esta função retorna uma ocorrência específica no banco de dados. </summary>**/
  public IResult Get(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.QueryFirstOrDefault<Ocorrencia>($"SELECT id_ocorrencia, data, local, UF, municipio, descricao, tipo, status, id_veiculo, id_cliente, id_terceirizado from Ocorrencias WHERE id_ocorrencia={id}");

    if (data == null) return Results.NotFound("Ocorrência não encontrada.");

    return Results.Ok(data);
  }
  /** <summary> Esta função retorna o documento de ocorrência específica no banco de dados. </summary>**/
  public IResult GetDocument(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    //TODO: Retornar documento das ocorrências.
    try
    {
      var data = connectionString.QueryFirstOrDefault<string>($"SELECT CAST(documento AS varchar(max)) from Ocorrencias WHERE id_ocorrencia={id}");

      if (data == null) return Results.NotFound("Ocorrência não encontrada, ou ocorrência não possui documento.");

      return Results.Ok(data);

    }
    catch (SystemException)
    {

      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }
  }

  /** <summary> Esta função insere uma ocorrência no banco de dados. </summary>**/
  public IResult Insert(Ocorrencia ocorrencia, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    try
    {
      // Verificando se alguma das propriedades da ocorrencia é nula ou vazia.
      //TODO: Terceirizado pode ser vazio. Não deve passar por essa validação.
      bool hasValidProperties = NullPropertyValidator.Validate(ocorrencia);
      if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

      var data = connectionString.Query<Veiculo>($"INSERT INTO Ocorrencias (data, local, UF, municipio, descricao, tipo, status, id_veiculo, id_cliente, id_terceirizado) VALUES ('{ocorrencia.data}', '{ocorrencia.local}', '{ocorrencia.UF}', '{ocorrencia.municipio}', '{ocorrencia.descricao}', '{ocorrencia.tipo}','{ocorrencia.status}', '{ocorrencia.id_veiculo}', '{ocorrencia.id_cliente}', '{ocorrencia.id_terceirizado}')");

      return Results.StatusCode(201);
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
  /** <summary> Esta função insere o documento de uma ocorrência no banco de dados. </summary>**/
  public async Task<IResult> InsertDocument(int id, HttpRequest sentFile, string dbConnectionString)
  {
    //TODO: Fazer documento aceito ser arquivo em foramto png, jpg, jpeg, pdf. Por enquanto só aceita arquivo txt.
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    if (!sentFile.HasFormContentType)
      return Results.BadRequest("Formato de requisição inválido.");

    var form = await sentFile.ReadFormAsync();
    var formFile = form.Files["file"];

    if (formFile is null || formFile.Length == 0)
      return Results.BadRequest("Arquivo enviado não pode ser vazio.");

    await using var stream = formFile.OpenReadStream();

    var reader = new StreamReader(stream);
    var file = await reader.ReadToEndAsync();
    try
    {
      var data = connectionString.Query<Veiculo>($"UPDATE Ocorrencias SET documento=CONVERT(varbinary(max),'{file}') WHERE id_ocorrencia={id}");
      return Results.StatusCode(201);
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }
  }

}