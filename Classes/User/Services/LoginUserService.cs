static class LoginUserService
{
  /** <summary> Esta função realiza o login do usuario. </summary>**/
  public static IResult Login(string email, string password, SqlConnection connectionString, WebApplicationBuilder builder)
  {
    try
    {
      string hashPassword = GetUserHashPasswordByEmailRepository.Get(email: email, connectionString: connectionString);
      if (hashPassword == null) return Results.BadRequest("E-mail ou senha inválidos.");

      // Verificando senha do usuario.
      bool isValid = PasswordHasher.Verify(hashPassword, password);
      if (!isValid) return Results.BadRequest("E-mail ou senha inválidos.");

      // Gerando token.
      var issuer = builder.Configuration["Jwt:Issuer"];
      var audience = builder.Configuration["Jwt:Audience"];
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]));
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(issuer: issuer, audience: audience, signingCredentials: credentials);

      var tokenHandler = new JwtSecurityTokenHandler();
      var stringToken = tokenHandler.WriteToken(token);

      return Results.Ok(stringToken);
    }
    catch (SystemException)
    {
      return Results.BadRequest("Houve um erro ao processar sua requisição. Tente novamente mais tarde.");
    }
  }
}