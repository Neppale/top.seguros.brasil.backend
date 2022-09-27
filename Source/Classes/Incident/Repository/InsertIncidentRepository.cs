static class InsertIncidentRepository
{
    public static async Task<GetIncidentByIdDto?> Insert(Ocorrencia incident, SqlConnection connectionString)
    {
        try
        {
            await connectionString.QueryAsync("INSERT INTO Ocorrencias (data, local, UF, municipio, descricao, tipo, id_veiculo, id_cliente, id_terceirizado) VALUES (@Data, @Local, @UF, @Municipio, @Descricao, @Tipo,  @IdVeiculo, @IdCliente, @IdTerceirizado)", new { Data = incident.data, Local = incident.local, UF = incident.UF, Municipio = incident.municipio, Descricao = incident.descricao, Tipo = incident.tipo, IdVeiculo = incident.id_veiculo, IdCliente = incident.id_cliente, IdTerceirizado = incident.id_terceirizado });

            var createdIncident = await connectionString.QueryFirstOrDefaultAsync<GetIncidentByIdDto>("SELECT id_ocorrencia, data, local, UF, municipio, descricao, tipo, id_veiculo, id_cliente, id_terceirizado FROM Ocorrencias WHERE id_ocorrencia = (SELECT MAX(id_ocorrencia) FROM Ocorrencias)");

            return createdIncident;
        }
        catch (SystemException)
        {
            return null;
        }
    }
}