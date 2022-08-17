static class InsertUserService
{
  /** <summary> Esta função insere um Usuario no banco de dados. </summary>**/
  public static IResult Insert(dynamic usuario, SqlConnection connectionString)
  {
    bool hasValidProperties = NullPropertyValidator.Validate(usuario);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    usuario.status = true;

    bool userIsValid = UserAlreadyExistsValidator.Validate(email: usuario.email, connectionString: connectionString);
    if (!userIsValid) return Results.BadRequest("O e-mail informado já está sendo utilizado em outra conta.");

    bool passwordIsValid = PasswordValidator.Validate(usuario.senha);
    if (!passwordIsValid) return Results.BadRequest("A senha informada não corresponde aos requisitos de segurança.");
    
    usuario.senha = PasswordHasher.HashPassword(usuario.senha);

    var result = InsertUserRepository.Insert(user: usuario, connectionString: connectionString);
    if (result == 0) return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");

    return Results.Created($"/usuario/{result}", new { id_usuario = result });
  }
}