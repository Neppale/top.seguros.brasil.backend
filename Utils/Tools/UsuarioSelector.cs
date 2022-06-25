static class UsuarioSelector
{
  static public int Select(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Retornar o usuário que possuir menos apólices associadas à ele.
    //TODO: Usuário com status inativo não deve ser escolhido.
    int leastApoliceUsuarioId = connectionString.QueryFirstOrDefault<int>("SELECT Usuarios.id_usuario, COUNT(Apolices.id_apolice) AS apolices FROM Usuarios LEFT JOIN Apolices ON Apolices.id_usuario = Usuarios.id_usuario GROUP BY Usuarios.id_usuario ORDER BY apolices ASC");

    return leastApoliceUsuarioId;

  }
}