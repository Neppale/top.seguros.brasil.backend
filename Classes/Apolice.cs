using Dapper;
using Microsoft.Data.SqlClient;
using tsb.mininal.policy.engine.Utils;
public class Apolice
{
  public int id_apolice { get; set; }
  public string data_inicio { get; set; }
  public string data_fim { get; set; }
  public double premio { get; set; }
  public double indenizacao { get; set; }
  public int id_cobertura { get; set; }
  public int id_usuario { get; set; }
  public int id_cliente { get; set; }
  public int id_veiculo { get; set; }
  public string status { get; set; }

  public Apolice(string startDate, string endDate, double premium, double indemnity, int idcobertura, int idusuario, int idcliente, int idveiculo, string status)
  {

    this.data_inicio = startDate;
    this.data_fim = endDate;
    this.premio = premium;
    this.indenizacao = indemnity;
    this.id_cobertura = idcobertura;
    this.id_usuario = idusuario;
    this.id_cliente = idcliente;
    this.id_veiculo = idveiculo;
    this.status = status;
  }

  public Apolice()
  {
  }

  /** <summary> Esta função retorna as apólices no banco de dados. </summary>**/
  public IResult Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    var data = connectionString.QueryFirstOrDefault<Apolice>("SELECT * from Apolices");

    return Results.Ok(data);
  }

  /** <summary> Esta função retorna uma apólice específica no banco de dados. </summary>**/
  public IResult Get(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.QueryFirstOrDefault<Apolice>("SELECT * from Apolices WHERE id_apolice = @Id", new { Id = id });

    if (data == null) return Results.NotFound("Apólice não encontrada.");

    return Results.Ok(data);
  }

  /** <summary> Esta função insere uma apólice no banco de dados. </summary>**/
  public IResult Insert(Apolice apolice, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se alguma das propriedades do Veiculo é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(apolice);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    if (apolice.indenizacao == 0) return Results.BadRequest("Valor de indenização não pode ser 0.");
    if (apolice.premio == 0) return Results.BadRequest("Valor de prêmio não pode ser 0.");

    try
    {
      connectionString.Query<Apolice>("INSERT INTO Apolices (data_inicio, data_fim, premio, indenizacao, id_cobertura, id_usuario, id_cliente, id_veiculo, status) VALUES (@DataInicio, @DataFim, @Premio, @Indenizacao, @IdCobertura, @IdUsuario, @IdCliente, @IdVeiculo, @Status)", new { DataInicio = apolice.data_inicio, DataFim = apolice.data_fim, Premio = apolice.premio, Indenizacao = apolice.indenizacao, IdCobertura = apolice.id_cobertura, IdUsuario = apolice.id_usuario, IdCliente = apolice.id_cliente, IdVeiculo = apolice.id_veiculo, Status = apolice.status });

      return Results.StatusCode(201);
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
  /** <summary> Esta função altera o status de uma apólice no banco de dados. </summary>**/
  public IResult Update(int id, Apolice apolice, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se alguma das propriedades do apolice é nula.
    bool hasValidProperties = NullPropertyValidator.Validate(apolice);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    try
    {
      connectionString.Query<Apolice>("UPDATE Apolices SET status = @Status' WHERE id_apolice = @Id", new { Id = id, Status = apolice.status });
      return Results.Ok();
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }
  }
}