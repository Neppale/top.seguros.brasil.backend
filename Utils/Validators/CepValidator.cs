
namespace tsb.mininal.policy.engine.Utils;

public static class CepValidator
{

  private static readonly HttpClient client = new HttpClient();
  public static async Task<bool> Validate(string cep)
  {
    HttpResponseMessage response = await client.GetAsync($"https://viacep.com.br/ws/{cep}/json/");

    if (response.StatusCode != HttpStatusCode.OK) return false;
    if (response.Content.ReadAsStringAsync().Result.Contains("erro")) return false;

    return true;
  }
}


