static class GetAllClientRepository
{
  public static IEnumerable<dynamic> Get(SqlConnection connectionString, int? pageNumber)
  {
    return connectionString.Query<dynamic>("SELECT id_cliente, nome_completo, email, cpf, telefone1 FROM Clientes WHERE status = 'true' ORDER BY id_cliente OFFSET @PageNumber ROWS FETCH NEXT 5 ROWS ONLY", new { PageNumber = (pageNumber - 1) * 5 });
  }
}