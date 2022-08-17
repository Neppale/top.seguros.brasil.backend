static class InsertIncidentDocumentRepository
{
  public static int Insert(int id, string fileBase64, string fileType, SqlConnection connectionString)
  {
    try
    {
      connectionString.Query("UPDATE Ocorrencias SET documento = @File, tipoDocumento = @FileType WHERE id_ocorrencia = @Id", new { File = fileBase64, Id = id, FileType = fileType });
      return 1;
    }
    catch (SystemException)
    {
      return 0;
    }
  }
}