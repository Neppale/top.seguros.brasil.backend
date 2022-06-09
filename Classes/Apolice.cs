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
    public IEnumerable<Apolice> Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    var data = connectionString.Query<Apolice>("SELECT * from Apolices");

    Console.WriteLine("[INFO] A request for all 'apolices' was made. The response is not a mock. :)");

    return data;
  }

  /** <summary> Esta função retorna uma apólice específica no banco de dados. </summary>**/
  public IEnumerable<Apolice> Get(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.Query<Apolice>($"SELECT * from Apolices WHERE id_apolice={id}");

    Console.WriteLine("[INFO] A request for a single 'apolice' was made. The response is not a mock. :)");

    if (data.Count() == 0) throw new BadHttpRequestException("Ap�lice n�o encontrada.", statusCode: 404);

    return data;
  }

  /** <summary> Esta função insere uma apólice no banco de dados. </summary>**/
  public IResult Insert(Apolice apolice, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    Console.WriteLine("[INFO] A request to post to 'apolices' was made :)");

    bool NullProperty = apolice.GetType().GetProperties()
                              .All(p => p.GetValue(apolice) != null);
    if (!NullProperty) return Results.BadRequest("Há um campo inválido na sua requisição.");

    if (apolice.indenizacao == 0) return Results.BadRequest("Valor de indenização não pode ser 0.");
    if (apolice.premio == 0) return Results.BadRequest("Valor de prêmio não pode ser 0.");

    try
    {


      var data = connectionString.Query<Cliente>($"INSERT INTO Apolices (data_inicio, data_fim, premio, indenizacao, id_cobertura, id_usuario, id_cliente, id_veiculo, status) VALUES ('{apolice.data_inicio}', '{apolice.data_fim}', '{apolice.premio}', '{apolice.indenizacao}', '{apolice.id_cobertura}', '{apolice.id_usuario}', '{apolice.id_cliente}', '{apolice.id_veiculo}', '{apolice.status}')");

      return Results.StatusCode(201);
    }
    catch (BadHttpRequestException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}