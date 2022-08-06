
static class GetPolicyByUserRepository
{
  public static IEnumerable<Apolice> Get(int id, SqlConnection connectionString, int? pageNumber)
  {
    var policies = connectionString.Query<Apolice>("SELECT id_apolice, Clientes.nome_completo AS nome, Coberturas.nome AS tipo, Veiculos.modelo AS veiculo, Apolices.status FROM Apolices LEFT JOIN Clientes ON Clientes.id_cliente = Apolices.id_cliente LEFT JOIN Coberturas ON Coberturas.id_cobertura = Apolices.id_cobertura LEFT JOIN Veiculos ON Veiculos.id_veiculo = Apolices.id_veiculo WHERE Apolices.id_usuario = @Id ORDER BY id_apolice OFFSET @PageNumber ROWS FETCH NEXT 5 ROWS ONLY", new { Id = id, PageNumber = (pageNumber - 1) * 5 });

    return policies;
  }
}