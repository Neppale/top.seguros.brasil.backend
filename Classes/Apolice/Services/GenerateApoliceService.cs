static class GenerateApoliceService
{

  public static IResult Generate(int id_cliente, int id_veiculo, int id_cobertura, string dbConnectionString)
  {
    Apolice generatedApolice = new();

    generatedApolice.data_inicio = DateTime.Now.ToString();
    generatedApolice.data_fim = DateTime.Now.AddYears(1).ToString();
    generatedApolice.indenizacao = PolicyGenerator.GenerateIndenizacao(id_veiculo);
    generatedApolice.premio = PolicyGenerator.GeneratePremio(id_veiculo);
    generatedApolice.id_cliente = id_cliente;
    generatedApolice.id_cobertura = id_cobertura;
    generatedApolice.id_veiculo = id_veiculo;
    generatedApolice.id_usuario = PolicyGenerator.ChooseUsuario(dbConnectionString);
    generatedApolice.status = "Em Análise";

    return InsertApoliceService.Insert(apolice: generatedApolice, dbConnectionString: dbConnectionString);
  }
}