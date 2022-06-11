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

  public IEnumerable<Ocorrencia> Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.Query<Ocorrencia>("SELECT id_ocorrencia, data, local, UF, municipio, descricao, tipo, status, id_veiculo, id_cliente, id_terceirizado from Ocorrencias");

    return data;
  }

  /** <summary> Esta função retorna uma ocorrecia específica no banco de dados. </summary>**/
  public IEnumerable<Ocorrencia> Get(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.Query<Ocorrencia>($"SELECT id_ocorrencia, data, local, UF, municipio, descricao, tipo, status, id_veiculo, id_cliente, id_terceirizado from Ocorrencias WHERE id_ocorrencia={id}");

    if (data.Count() == 0) throw new BadHttpRequestException("Ocorrencia não encontrada.", statusCode: 404);

    return data;
  }
}