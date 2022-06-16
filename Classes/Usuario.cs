using Dapper;
using Microsoft.Data.SqlClient;
using tsb.mininal.policy.engine.Utils;
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
  public IResult Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.Query<Usuario>("SELECT * from Usuarios WHERE status='true'");

    return Results.Ok(data);
  }
  /** <summary> Esta função retorna um usuário específico no banco de dados. </summary>**/
  public IResult Get(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.QueryFirstOrDefault<Usuario>($"SELECT * from Usuarios WHERE id_Usuario={id}");

    if (data == null) return Results.NotFound("Usuário não encontrado.");

    return Results.Ok(data);
  }
  /** <summary> Esta função insere um Usuario no banco de dados. </summary>**/
  public IResult Insert(Usuario usuario, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    try
    {
      // Verificando se alguma das propriedades do Usuario é nula ou vazia.
      bool isValid = NullPropertyValidator.Validate(usuario);
      if (!isValid) return Results.BadRequest("Há um campo inválido na sua requisição.");

      // Criptografando a senha do usuário.
      usuario.senha = PasswordHasher.HashPassword(usuario.senha);

      connectionString.Query($"INSERT INTO Usuarios (nome_completo, email, senha, tipo, status) VALUES ('{usuario.nome_completo}', '{usuario.email}', '{usuario.senha}', '{usuario.tipo}', '{usuario.status}')");

      return Results.StatusCode(200);
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }

  /** <summary> Esta função altera um Usuario no banco de dados. </summary>**/
  public IResult Update(int id, Usuario usuario, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se alguma das propriedades do Usuario é nula ou vazia.
    bool isValid = NullPropertyValidator.Validate(usuario);
    if (!isValid) return Results.BadRequest("Há um campo inválido na sua requisição.");

    // Criptografando a senha do usuário.
    usuario.senha = PasswordHasher.HashPassword(usuario.senha);

    try
    {

      connectionString.Query($"UPDATE Usuarios SET nome_completo = '{usuario.nome_completo}', email = '{usuario.email}', senha = '{usuario.senha}', tipo = '{usuario.tipo}', status = '{usuario.status}' WHERE id_usuario = {id}");
      return Results.Ok();
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }

  public IResult Login(string email, string password, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    try
    {

      string hashPassword = connectionString.QueryFirstOrDefault<string>($"SELECT senha FROM Usuarios WHERE email = '{email}' ");

      if (hashPassword == null) return Results.BadRequest("E-mail ou senha inválidos.");

      // Verificando senha do cliente.
      bool isValid = PasswordHasher.Verify(hashPassword, password);
      if (!isValid) return Results.BadRequest("E-mail ou senha inválidos.");

      return Results.Ok();
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }
  }
}