static class InsertUserService
{
  /** <summary> Esta função insere um Usuario no banco de dados. </summary>**/
  public static async Task<IResult> Insert(Usuario usuario, SqlConnection connectionString)
  {
    bool hasValidProperties = NullPropertyValidator.Validate(usuario);
    if (!hasValidProperties) return Results.BadRequest(new { message = "Há um campo inválido na sua requisição." });

    usuario.status = true;

    bool userIsValid = await UserAlreadyExistsValidator.Validate(email: usuario.email, connectionString: connectionString);
    if (!userIsValid) return Results.BadRequest(new { message = "O e-mail informado já está sendo utilizado em outra conta." });

    bool passwordIsValid = PasswordValidator.Validate(usuario.senha);
    if (!passwordIsValid) return Results.BadRequest(new { message = "A senha informada não corresponde aos requisitos de segurança." });

    usuario.senha = PasswordHasher.HashPassword(usuario.senha);

    var createdUser = await InsertUserRepository.Insert(user: usuario, connectionString: connectionString);
    if (createdUser == null) return Results.BadRequest(new { message = "Houve um erro ao processar sua requisição. Tente novamente mais tarde." });

    return Results.Created($"/usuario/{createdUser.id_usuario}", new { message = "Usuário criado com sucesso.", user = createdUser });
  }
}