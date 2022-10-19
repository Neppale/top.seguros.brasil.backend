static class GetCepInfo
{
    private static readonly HttpClient client = new HttpClient();
    public static async Task<CepInfo> Get(string cep)
    {
        HttpResponseMessage response = await client.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
        var cepInfo = JsonSerializer.Deserialize<CepInfo>(await response.Content.ReadAsStringAsync());

        return cepInfo;
    }
}