static class InsertOutsourcedRepository
{
  public static int Insert(Terceirizado outsourced, SqlConnection connectionString)
  {
    try
    {
      connectionString.Query("INSERT INTO Terceirizados (nome, funcao, cnpj, telefone, valor) VALUES (@Nome, @Funcao, @Cnpj, @Telefone, @Valor)", new { Nome = outsourced.nome, Funcao = outsourced.funcao, Cnpj = outsourced.cnpj, Telefone = outsourced.telefone, Valor = outsourced.valor });

      // Pegando o ID do Terceirizado que acabou de ser inserido.
      int createdTerceirizadoId = connectionString.QueryFirstOrDefault<int>("SELECT id_terceirizado FROM Terceirizados WHERE cnpj = @Cnpj", new { Cnpj = outsourced.cnpj });

      return createdTerceirizadoId;
    }
    catch (SystemException)
    {
      return 0;
    }
  }
}