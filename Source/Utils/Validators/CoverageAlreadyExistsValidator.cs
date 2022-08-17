static class CoverageAlreadyExistsValidator
{
  public static bool Validate(string name, SqlConnection connectionString)
  {
    bool nameAlreadyExists = connectionString.QueryFirstOrDefault<bool>("SELECT CASE WHEN EXISTS (SELECT nome FROM Coberturas WHERE nome = @Nome AND status = 'true') THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Nome = name });
    if (nameAlreadyExists) return false;
    return true;
  }

  public static bool Validate(int id, string name, SqlConnection connectionString)
  {
    bool nameAlreadyExists = connectionString.QueryFirstOrDefault<bool>("SELECT CASE WHEN EXISTS (SELECT nome FROM Coberturas WHERE nome = @Nome AND status = 'true' AND id_cobertura != @Id) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Nome = name, Id = id });
    if (nameAlreadyExists) return false;
    return true;
  }
}