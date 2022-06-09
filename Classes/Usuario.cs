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
  }

  public Usuario(string fullName, string email, string type, string password, bool status)
  {
    this.nome_completo = fullName;
    this.email = email;
    this.tipo = type;
    this.senha = password;
    this.status = status;
  }

  /** <summary> Esta função retorna todos os usuários no banco de dados. </summary>**/
  public IEnumerable<Usuario> Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.Query<Usuario>("SELECT * from Usuarios");

    Console.WriteLine("[INFO] A request for all 'Usuarios' was made. The response is not a mock. :)");

    return data;
  }
  /** <summary> Esta função retorna um usuário específico no banco de dados. </summary>**/
  public IEnumerable<Usuario> Get(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.Query<Usuario>($"SELECT * from Usuarios WHERE id_Usuario={id}");

    Console.WriteLine("[INFO] A request for a single 'Usuario' was made. The response is not a mock. :)");
    if (data.Count() == 0) throw new BadHttpRequestException("Usuario não encontrado.", statusCode: 404);

    return data;
  }
  /** <summary> Esta função insere um Usuario no banco de dados. </summary>**/
  public IResult Insert(Usuario usuario, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    Console.WriteLine("[INFO] A request to post to 'Usuarios' was made :)");

    try
    {
      // Verificando se alguma das propriedades do Usuario é nulo.
      bool NullProperty = usuario.GetType().GetProperties()
                              .All(p => p.GetValue(usuario) != null);
      if (!NullProperty) return Results.BadRequest("Há um campo inválido na sua requisição.");

      var data = connectionString.Query<Usuario>($"INSERT INTO Usuarios (nome_completo, email, senha, tipo, status) VALUES ('{usuario.nome_completo}', '{usuario.email}', '{usuario.senha}', '{usuario.tipo}', '{usuario.status}')");

      return Results.StatusCode(201);
    }
    catch (BadHttpRequestException)
    {
      //TODO: Exception Handler para mostrar o erro/statusCode correto com base na mensagem enviada pelo SQL server.
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}