static class UpdateOutsourcedRepository
{
  public static int Update(int id, Terceirizado outsourced, SqlConnection connectionString)
  {
    try
    {
      connectionString.Query("UPDATE Terceirizados SET nome = @Nome, funcao = @Funcao, cnpj = @Cnpj, telefone = @Telefone, valor = @Valor WHERE id_terceirizado = @Id", new { Nome = outsourced.nome, Funcao = outsourced.funcao, Cnpj = outsourced.cnpj, Telefone = outsourced.telefone, Valor = outsourced.valor, Id = id });

      return 1;
    }
    catch (SystemException)
    {
      return 0;
    }
  }
}