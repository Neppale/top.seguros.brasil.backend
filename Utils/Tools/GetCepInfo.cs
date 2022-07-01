static class GetCepInfo
{
  private static readonly HttpClient client = new HttpClient();
  public static async Task<HttpContent> Get(string cep)
  {
    HttpResponseMessage response = await client.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
    return response.Content;
  }
}