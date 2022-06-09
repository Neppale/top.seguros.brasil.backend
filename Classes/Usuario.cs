using Dapper;
using Microsoft.Data.SqlClient;
public class Usuario
{
  public int id_usuario { get; set; }
  public string nome_completo { get; set; }
  public string email { get; set; }
  public string tipo { get; set; }

  public string senha;
  public bool status { get; set; }

  public Usuario()
  {
    id_usuario = 0;
    nome_completo = "any_fullName";
    email = "any_email";
    tipo = "any_tipo";
    senha = "any_password";
    status = true;

  }

  public Usuario(string fullName, string email, string type, string password, bool status)
  {
    this.nome_completo = fullName;
    this.email = email;
    this.tipo = type;
    this.senha = password;
    this.status = status;
  }
  public IEnumerable<Usuario> Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.Query<Usuario>("SELECT * from Usuarios");

    Console.WriteLine("[INFO] A request for all 'Usuarios' was made. The response is not a mock. :)");

    return data;
  }
  // Esta função retorna apenas um Usuario em específico.
  public IEnumerable<Usuario> Get(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.Query<Usuario>($"SELECT * from Usuarios WHERE id_Usuario={id}");

    Console.WriteLine("[INFO] A request for a single 'Usuario' was made. The response is not a mock. :)");
    if (data.Count() == 0) throw new BadHttpRequestException("Usuario não encontrado.", statusCode: 404);

    return data;
  }
}