static class UpdateUserService
{
    /** <summary> Esta função altera um Usuario no banco de dados. </summary>**/
    public static async Task<IResult> Update(int id, Usuario usuario, SqlConnection connectionString)
    {
        bool hasValidProperties = NullPropertyValidator.Validate(usuario);
        if (!hasValidProperties) return Results.BadRequest(new { message = "Há um campo inválido na sua requisição." });

        var user = await GetUserByIdRepository.Get(id: id, connectionString: connectionString);
        if (user == null) return Results.NotFound(new { message = "Usuário não encontrado." });

        bool userIsValid = await UserAlreadyExistsValidator.Validate(id: id, email: usuario.email, connectionString: connectionString);
        if (!userIsValid) return Results.BadRequest(new { message = "O e-mail informado já está sendo utilizado em outra conta." });

        usuario.senha = PasswordHasher.HashPassword(usuario.senha);

        var updatedUser = await UpdateUserRepository.Update(id: id, usuario: usuario, connectionString: connectionString);
        if (updatedUser == null) return Results.BadRequest(new { message = "Houve um erro ao processar sua requisição. Tente novamente mais tarde." });

        return Results.Ok(new { message = "Usuário alterado com sucesso.", user = updatedUser });
    }
}

