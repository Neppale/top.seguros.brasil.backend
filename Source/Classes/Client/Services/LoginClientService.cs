static class LoginClientService
{
    /** <summary> Esta função faz o login do cliente. </summary>**/
    public static async Task<IResult> Login(ClientLoginDto login, SqlConnection connectionString, WebApplicationBuilder builder)
    {
        string hashPassword = await GetClientHashPasswordByEmailRepository.Get(email: login.email, connectionString: connectionString);
        if (hashPassword == null) return Results.BadRequest("E-mail ou senha inválidos.");

        bool isValid = PasswordHasher.Verify(hashPassword, login.senha);
        if (!isValid) return Results.BadRequest("E-mail ou senha inválidos.");

        var client = await GetClientByEmailRepository.Get(email: login.email, connectionString: connectionString);

        try
        {
            // Gerando token.
            var issuer = builder.Configuration["JwtIssuer"];
            var audience = builder.Configuration["JwtAudience"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: issuer, audience: audience, signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);

            return Results.Ok(new { client = client, token = stringToken });

        }
        catch (SystemException)
        {
            return Results.BadRequest(new { message = "Houve um erro ao processar sua requisição. Tente novamente mais tarde." });
        }
    }
}