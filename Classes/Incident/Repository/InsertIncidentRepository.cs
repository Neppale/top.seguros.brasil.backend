static class InsertIncidentRepository
{
  public static int Insert(Ocorrencia incident, SqlConnection connectionString)
  {
    try
    {
      connectionString.Query("INSERT INTO Ocorrencias (data, local, UF, municipio, descricao, tipo, id_veiculo, id_cliente, id_terceirizado) VALUES (@Data, @Local, @UF, @Municipio, @Descricao, @Tipo,  @IdVeiculo, @IdCliente, @IdTerceirizado)", new { Data = incident.data, Local = incident.local, UF = incident.UF, Municipio = incident.municipio, Descricao = incident.descricao, Tipo = incident.tipo, IdVeiculo = incident.id_veiculo, IdCliente = incident.id_cliente, IdTerceirizado = incident.id_terceirizado });

      // Pegando o id da ocorrência inserida para retornar na resposta.
      int createdOcorrenciaId = connectionString.QueryFirstOrDefault<int>("SELECT id_ocorrencia FROM Ocorrencias WHERE data = @Data AND local = @Local AND UF = @UF AND municipio = @Municipio AND descricao = @Descricao AND tipo = @Tipo AND id_veiculo = @IdVeiculo AND id_cliente = @IdCliente", new { Data = incident.data, Local = incident.local, UF = incident.UF, Municipio = incident.municipio, Descricao = incident.descricao, Tipo = incident.tipo, IdVeiculo = incident.id_veiculo, IdCliente = incident.id_cliente });

      return createdOcorrenciaId;
    }
    catch (SystemException)
    {
      return 0;
    }
  }
}