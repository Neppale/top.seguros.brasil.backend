
using System.Net;
namespace tsb.mininal.policy.engine.Utils;

public abstract class CepValidator
{
  private static readonly string[] blockedCeps = { "00000-000", "11111-111", "22222-222", "33333-333", "44444-444", "55555-555", "66666-666", "77777-777", "88888-888", "99999-999" };
  private static readonly HttpClient client = new HttpClient();
  public static async Task<bool> Validate(string cep)
  {
    HttpResponseMessage response = new();
    if (blockedCeps.Contains(cep))
    {
      response.StatusCode = HttpStatusCode.NotFound;
      return Verify(response);
    }

    response = await client.GetAsync($"https://viacep.com.br/ws/{cep}/json/");

    return Verify(response);
  }
  public static bool Verify(HttpResponseMessage response)
  {
    //TODO: Achar uma API melhor que a ViaCEP, que retorna CEPs inválidos com StatusCode 200 por algum motivo
    if (response.StatusCode != HttpStatusCode.OK) return false;
    return true;
  }

}


