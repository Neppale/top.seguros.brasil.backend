public static class DeleteOutsourcedService
{
  /** <summary> Esta função altera um terceirizado no banco de dados. </summary>**/
  public static IResult Delete(int id, SqlConnection connectionString)
  {
    var outsourced = GetOutsourcedByIdRepository.Get(id: id, connectionString: connectionString);
    if (outsourced == null) return Results.NotFound(new { message = "Terceirizado não encontrado" });

    var incidents = GetIncidentByOutsourcedRepository.Get(id: id, connectionString: connectionString, pageNumber: 1, size: int.MaxValue);
    if (incidents.Any(incident => incident.status == "Andamento")) return Results.BadRequest(new { message = "Não é possível desativar um terceirizado com ocorrências ativas." });

    var result = DeleteOutsourcedRepository.Delete(id: id, connectionString: connectionString);
    if (result == 0) return Results.BadRequest(new { message = "Houve um erro ao processar sua requisição. Tente novamente mais tarde." });

    return Results.NoContent();
  }
}