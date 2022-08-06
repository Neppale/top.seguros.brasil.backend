static class InsertUserService
{
  /** <summary> Esta função insere um Usuario no banco de dados. </summary>**/
  public static IResult Insert(dynamic usuario, SqlConnection connectionString)
  {
    // Verificando se alguma das propriedades do Usuario é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(usuario);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    // Por padrão, o status do usuário é true.
    usuario.status = true;

    // Verificando se e-mail já existe em outra conta no banco de dados.
    bool userIsValid = UserAlreadyExistsValidator.Validate(email: usuario.email, connectionString: connectionString);
    if (!userIsValid) return Results.BadRequest("O e-mail informado já está sendo utilizado em outra conta.");

    // Criptografando a senha do usuário.
    usuario.senha = PasswordHasher.HashPassword(usuario.senha);

    var result = InsertUserRepository.Insert(user: usuario, connectionString: connectionString);
    if (result == 0) return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");

    return Results.Created($"/usuario/{result}", new { id_usuario = result });
  }
}