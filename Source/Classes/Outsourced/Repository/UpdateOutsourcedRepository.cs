static class UpdateOutsourcedRepository
{
  public static Terceirizado? Update(int id, Terceirizado outsourced, SqlConnection connectionString)
  {
    try
    {
      connectionString.Query("UPDATE Terceirizados SET nome = @Nome, funcao = @Funcao, cnpj = @Cnpj, telefone = @Telefone, valor = @Valor WHERE id_terceirizado = @Id", new { Nome = outsourced.nome, Funcao = outsourced.funcao, Cnpj = outsourced.cnpj, Telefone = outsourced.telefone, Valor = outsourced.valor, Id = id });

      var updatedOutsourced = connectionString.QueryFirstOrDefault<Terceirizado>("SELECT * FROM Terceirizados WHERE id_terceirizado = @Id", new { Id = id });
      return updatedOutsourced;
    }
    catch (SystemException)
    {
      return null;
    }
  }
}