using Dapper;
using Microsoft.Data.SqlClient;
using tsb.mininal.policy.engine.Utils;

static class InsertUsuarioService
{
  /** <summary> Esta função insere um Usuario no banco de dados. </summary>**/
  public static IResult Insert(Usuario usuario, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se alguma das propriedades do Usuario é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(usuario);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    // Por padrão, o status do usuário é true.
    usuario.status = true;

    // Verificando se e-mail já existe em outra conta no banco de dados.
    bool emailAlreadyExists = connectionString.QueryFirstOrDefault<bool>("SELECT CASE WHEN EXISTS (SELECT email FROM Usuarios WHERE email = @Email) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Email = usuario.email });
    if (emailAlreadyExists) return Results.BadRequest("O e-mail informado já está sendo utilizado em outra conta.");

    // Criptografando a senha do usuário.
    usuario.senha = PasswordHasher.HashPassword(usuario.senha);

    try
    {
      connectionString.Query("INSERT INTO Usuarios (nome_completo, email, senha, tipo, status) VALUES (@Nome, @Email, @Senha, @Tipo, @Status)", new { Nome = usuario.nome_completo, Email = usuario.email, Senha = usuario.senha, Tipo = usuario.tipo, Status = usuario.status });

      // Pegando o ID do usuário que acabou de ser inserido.
      int createdUsuarioId = connectionString.QueryFirstOrDefault<int>("SELECT id_usuario FROM Usuarios WHERE email = @Email", new { Email = usuario.email });

      return Results.Created($"/usuario/{createdUsuarioId}", new { id_usuario = createdUsuarioId });
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}