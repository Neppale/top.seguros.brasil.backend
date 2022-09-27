static class GetPolicyDocumentRepository
{
  public static async Task<string> Get(int id, SqlConnection connectionString)
  {
    var policyDocument = await connectionString.QueryFirstOrDefaultAsync<string>("SELECT documento FROM Apolices WHERE id_apolice = @Id", new { Id = id });

    return policyDocument;
  }
}