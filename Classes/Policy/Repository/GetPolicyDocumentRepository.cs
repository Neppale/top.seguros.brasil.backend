static class GetPolicyDocumentRepository
{
  public static string Get(int id, SqlConnection connectionString)
  {
    var policyDocument = connectionString.QueryFirstOrDefault<string>("SELECT documento FROM Apolices WHERE id_apolice = @Id", new { Id = id });

    return policyDocument;
  }
}