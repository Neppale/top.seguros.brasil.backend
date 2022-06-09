using Dapper;
using Microsoft.Data.SqlClient;
using tsb.mininal.policy.engine.Utils;
public class Apolice
{
  public int id { get; set; }
  public string data_inicio { get; set; }
  public string data_fim { get; set; }
  public double premio { get; set; }
  public double indenizacao { get; set; }
  public int id_cobertura { get; set; }
  public int id_usuario { get; set; }
  public int id_cliente { get; set; }
  public int id_veiculo { get; set; }
  public string status { get; set; }

  public Apolice()
  {
    id = 1;
    data_inicio = DateFormatter.FormatDate();
    data_fim = DateFormatter.FormatDate();
    premio = 0.00;
    indenizacao = 0.00;
    id_cobertura = 0;
    id_usuario = 0;
    id_cliente = 0;
    id_veiculo = 0;
    status = "any_status";

  }

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
}