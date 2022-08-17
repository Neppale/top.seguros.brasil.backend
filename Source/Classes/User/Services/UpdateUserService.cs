static class UpdateUserService
{
  /** <summary> Esta função altera um Usuario no banco de dados. </summary>**/
  public static IResult Update(int id, Usuario usuario, SqlConnection connectionString)
  {
    // Verificando se alguma das propriedades do Usuario é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(usuario);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    var user = GetOneUserRepository.Get(id: id, connectionString: connectionString);
    if (user == null) return Results.NotFound("Usuário não encontrado.");

    // Verificando se e-mail já existe em outra conta no banco de dados.
    bool userIsValid = UserAlreadyExistsValidator.Validate(id: id, email: usuario.email, connectionString: connectionString);
    if (!userIsValid) return Results.BadRequest("O e-mail informado já está sendo utilizado em outra conta.");

    // Criptografando a senha do usuário.
    usuario.senha = PasswordHasher.HashPassword(usuario.senha);

    var result = UpdateUserRepository.Update(id: id, usuario: usuario, connectionString: connectionString);
    if (result == 0) return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");

    return Results.Ok("Usuário alterado com sucesso.");
  }
}

