static class OutsourcedAlreadyExistsValidator
{
  public static async Task<bool> Validate(Terceirizado outsourced, SqlConnection connectionString)
  {
    bool cnpjAlreadyExists = await connectionString.QueryFirstOrDefaultAsync<bool>("SELECT CASE WHEN EXISTS (SELECT cnpj FROM Terceirizados WHERE cnpj = @Cnpj AND status = 'true') THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Cnpj = outsourced.cnpj });
    if (cnpjAlreadyExists) return false;

    bool phoneAlreadyExists = await connectionString.QueryFirstOrDefaultAsync<bool>("SELECT CASE WHEN EXISTS (SELECT telefone FROM Terceirizados WHERE telefone = @Telefone AND status = 'true') THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Telefone = outsourced.telefone });
    if (phoneAlreadyExists) return false;

    return true;
  }

  public static async Task<bool> Validate(int id, Terceirizado outsourced, SqlConnection connectionString)
  {
    bool cnpjAlreadyExists = await connectionString.QueryFirstOrDefaultAsync<bool>("SELECT CASE WHEN EXISTS (SELECT cnpj FROM Terceirizados WHERE cnpj = @Cnpj AND status = 'true' AND id_terceirizado != @Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Cnpj = outsourced.cnpj, Id = id });
    if (cnpjAlreadyExists) return false;

    bool phoneAlreadyExists = await connectionString.QueryFirstOrDefaultAsync<bool>("SELECT CASE WHEN EXISTS (SELECT telefone FROM Terceirizados WHERE telefone = @Telefone AND status = 'true' AND id_terceirizado != @Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Telefone = outsourced.telefone, Id = id });
    if (phoneAlreadyExists) return false;

    return true;
  }
}