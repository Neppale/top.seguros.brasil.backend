static class UpdateIncidentRepository
{
    public static async Task<GetIncidentByIdDto?> Update(int id, Ocorrencia incident, SqlConnection connectionString)
    {
        try
        {
            await connectionString.QueryFirstOrDefaultAsync("UPDATE Ocorrencias SET data = @Data, local = @Local, UF = @UF, municipio = @Municipio, descricao = @Descricao, tipo = @Tipo, status = @Status, id_veiculo = @Id_veiculo, id_cliente = @Id_cliente, id_terceirizado = @Id_terceirizado WHERE id_ocorrencia = @Id", new { Id = id, Data = incident.data, Local = incident.local, UF = incident.UF, Municipio = incident.municipio, Descricao = incident.descricao, Tipo = incident.tipo, Status = incident.status, Id_veiculo = incident.id_veiculo, Id_cliente = incident.id_cliente, Id_terceirizado = incident.id_terceirizado });

            var updatedIncident = await connectionString.QueryFirstOrDefaultAsync<GetIncidentByIdDto>("SELECT id_ocorrencia, data, local, UF, municipio, descricao, tipo, status, id_veiculo, id_cliente, id_terceirizado FROM Ocorrencias WHERE id_ocorrencia = @Id", new { Id = id });

            updatedIncident.data = SqlDateConverter.ConvertToShow(updatedIncident.data);

            return updatedIncident;
        }
        catch (System.Exception)
        {
            return null;
        }
    }
}