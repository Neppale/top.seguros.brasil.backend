using Dapper;
using Microsoft.Data.SqlClient;
public class Terceirizado
{
  public int id_terceirizado { get; set; }
  public string nome { get; set; }
  public string funcao { get; set; }
  public string cnpj { get; set; }
  public string telefone { get; set; }

  public double valor { get; set; }
  public bool status { get; set; }

  public Terceirizado()
  {
    id_terceirizado = 0;
    nome = "any_fullName";
    cnpj = "any_cnpj";
    funcao = "any_function";
    telefone = "any_phone";
    valor = 0.00;
    status = true;

  }

  public Terceirizado(string fullName, string function, string cnpj, string phone, double price, bool status)
  {
    this.nome = fullName;
    this.funcao = function;
    this.cnpj = cnpj;
    this.telefone = phone;
    this.valor = price;
    this.status = status;
  }

  /** <summary> Esta função retorna todos os terceirizados no banco de dados. </summary>**/
  public IEnumerable<Terceirizado> Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.Query<Terceirizado>("SELECT * from Terceirizados");

    Console.WriteLine("[INFO] A request for all 'terceirizados' was made. The response is not a mock. :)");

    return data;
  }
  /** <summary> Esta função retorna um terceirizado específico no banco de dados. </summary>**/
  public IEnumerable<Terceirizado> Get(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.Query<Terceirizado>($"SELECT * from Terceirizados WHERE id_terceirizado={id}");

    Console.WriteLine("[INFO] A request for a single 'terceirizado' was made. The response is not a mock. :)");
    if (data.Count() == 0) throw new BadHttpRequestException("Terceirizado não encontrado.", statusCode: 404);

    return data;
  }
}