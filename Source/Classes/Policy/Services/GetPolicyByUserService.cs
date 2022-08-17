static class GetPolicyByUserService
{
  public static IResult Get(int id_usuario, int? pageNumber, SqlConnection connectionString)
  {
    if (pageNumber == null) pageNumber = 1;

    var data = GetPolicyByUserRepository.Get(id: id_usuario, connectionString: connectionString, pageNumber: pageNumber);
    if (data.Count() == 0) return Results.NotFound("Nenhuma apólice encontrada para o usuário, ou usuário não existe.");

    return Results.Ok(data);

    // Exemplo de retorno, adaptada para o Management Stage:
    // [{
    //   "id_apolice": 1,
    //   "nome": "João da Silva",
    //   "tipo": "Premium",
    //   "veiculo": "Uno",
    //   "status": "Ativa"
    //   }]
  }
}
