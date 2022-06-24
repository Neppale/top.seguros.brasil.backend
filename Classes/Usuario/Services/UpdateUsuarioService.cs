static class UpdateUsuarioService
{
  /** <summary> Esta função altera um Usuario no banco de dados. </summary>**/
  public static IResult Update(int id, Usuario usuario, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se alguma das propriedades do Usuario é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(usuario);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    bool Exists = connectionString.QueryFirstOrDefault<bool>("SELECT id_usuario FROM Usuarios WHERE id_usuario = @Id", new { Id = id });
    if (!Exists) return Results.NotFound("Usuário não encontrado.");

    // Verificando se e-mail já existe em outra conta no banco de dados.
    bool emailAlreadyExists = connectionString.QueryFirstOrDefault<bool>("SELECT CASE WHEN EXISTS (SELECT email FROM Usuarios WHERE email = @Email AND id_usuario != @Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Email = usuario.email, Id = id });
    if (emailAlreadyExists) return Results.BadRequest("O e-mail informado já está sendo utilizado em outra conta.");

    // Criptografando a senha do usuário.
    usuario.senha = PasswordHasher.HashPassword(usuario.senha);

    try
    {
      connectionString.Query("UPDATE Usuarios SET nome_completo = @Nome, email = @Email, senha = @Senha, tipo = @Tipo WHERE id_Usuario = @Id", new { Nome = usuario.nome_completo, Email = usuario.email, Senha = usuario.senha, Tipo = usuario.tipo, Id = id });

      return Results.Ok();
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}