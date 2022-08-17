static class GetAllClientRepository
{
  public static IEnumerable<GetAllClientDto> Get(SqlConnection connectionString, int? pageNumber)
  {
    return connectionString.Query<GetAllClientDto>("SELECT id_cliente, nome_completo, email, cpf, telefone1 FROM Clientes WHERE status = 'true' ORDER BY id_cliente OFFSET @PageNumber ROWS FETCH NEXT 5 ROWS ONLY", new { PageNumber = (pageNumber - 1) * 5 });
  }
}