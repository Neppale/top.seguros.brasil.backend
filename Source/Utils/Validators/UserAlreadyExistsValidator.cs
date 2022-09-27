static class UserAlreadyExistsValidator
{
  public static async Task<bool> Validate(string email, SqlConnection connectionString)
  {
    bool emailAlreadyExists = await connectionString.QueryFirstOrDefaultAsync<bool>("SELECT CASE WHEN EXISTS (SELECT email FROM Usuarios WHERE email = @Email AND status = 'true') THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Email = email });
    if (emailAlreadyExists) return false;
    return true;
  }

  public static async Task<bool> Validate(int id, string email, SqlConnection connectionString)
  {
    bool emailAlreadyExists = await connectionString.QueryFirstOrDefaultAsync<bool>("SELECT CASE WHEN EXISTS (SELECT email FROM Usuarios WHERE email = @Email AND status = 'true' AND id_usuario != @Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Email = email, Id = id });
    if (emailAlreadyExists) return false;
    return true;
  }
}