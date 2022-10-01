static class PolicyDocumentGenerator
{
    /**<summary> Esta função gera um arquivo HTML da apólice. </summary>**/
    public static async Task<Stream> Generate(Apolice apolice, SqlConnection connectionString)
    {
        var user = await GetUserByIdRepository.Get(id: apolice.id_usuario, connectionString: connectionString);
        var client = await GetClientByIdRepository.Get(id: apolice.id_cliente, connectionString: connectionString);
        var vehicle = await GetVehicleByIdRepository.Get(id: apolice.id_veiculo, connectionString: connectionString);
        var coverage = await GetCoverageByIdRepository.Get(id: apolice.id_cobertura, connectionString: connectionString);
        var localization = await GetCepInfo.Get(client.cep);
        decimal veiculoPreco = await VehiclePriceFinder.Find(vehicle.marca, vehicle.modelo, vehicle.ano);

        string htmlDocument = await GetHtmlDocument();
        htmlDocument = FormatHtmlDocument(apolice, user, client, vehicle, coverage, localization, veiculoPreco, htmlDocument);

        return await GeneratePDF(htmlDocument);
    }

    private static async Task<string> GetHtmlDocument()
    {
        using (HttpClient httpClient = new())
        {
            HttpResponseMessage response = await httpClient.GetAsync($"https://verissimo.dev/api/PolicyDocument.html");
            string htmlDocument = await response.Content.ReadAsStringAsync();
            return htmlDocument;
        }
    }

    private static string FormatHtmlDocument(Apolice apolice, GetUserDto user, GetClientDto client, Veiculo vehicle, Cobertura coverage, CepInfo localization, decimal veiculoPreco, string htmlDocument)
    {
        htmlDocument = htmlDocument.Replace("{{DATAHOJE}}", DateTime.Now.ToString("dd/MM/yyyy"))
                                     .Replace("{{IDAPOLICE}}", apolice.id_apolice.ToString())
                                     .Replace("{{DATAINICIAL}}", apolice.data_inicio.Substring(8, 2) + "/" + apolice.data_inicio.Substring(5, 2) + "/" + apolice.data_inicio.Substring(0, 4))
                                     .Replace("{{DATAFINAL}}", apolice.data_fim.Substring(8, 2) + "/" + apolice.data_fim.Substring(5, 2) + "/" + apolice.data_fim.Substring(0, 4))
                                     .Replace("{{NOMEUSUARIO}}", user.nome_completo)
                                     .Replace("{{IDUSUARIO}}", user.id_usuario.ToString())
                                     .Replace("{{NOMECLIENTE}}", client.nome_completo)
                                     .Replace("{{CPFCLIENTE}}", client.cpf)
                                     .Replace("{{CEPCLIENTE}}", client.cep)
                                     .Replace("{{ENDERECOCLIENTE}}", $"{localization.logradouro}, {localization.bairro}, {localization.localidade}")
                                     .Replace("{{UFCLIENTE}}", localization.uf)
                                     .Replace("{{MARCAVEICULO}}", vehicle.marca)
                                     .Replace("{{MODELOVEICULO}}", vehicle.modelo)
                                     .Replace("{{PLACAVEICULO}}", vehicle.placa)
                                     .Replace("{{COMBUSTIVELVEICULO}}", vehicle.ano[(vehicle.ano.IndexOf(" ") + 1)..])
                                     .Replace("{{ANOVEICULO}}", vehicle.ano.Substring(0, vehicle.ano.IndexOf(" ")))
                                     .Replace("{{USOVEICULO}}", vehicle.uso)
                                     .Replace("{{DESCRICAOCOBERTURA}}", coverage.descricao)
                                     .Replace("{{COBERTURAVALOR}}", coverage.valor.ToString())
                                     .Replace("{{TAXAINDENIZACAOCOBERTURA}}", coverage.taxa_indenizacao.ToString())
                                     .Replace("{{VALORVEICULOFIPE}}", veiculoPreco.ToString())
                                     .Replace("{{PREMIOAPOLICE}}", apolice.premio.ToString())
                                     .Replace("{{INDENIZACAOAPOLICE}}", apolice.indenizacao.ToString());
        return htmlDocument;
    }

    private static async Task<Stream> GeneratePDF(string htmlDocument)
    {
        var tryNumber = 0;
        var requestBody = new
        {
            pdf = new
            {
                format = "A4",
                printBackground = true,
                scale = 1,
            },
            source = new
            {
                html = htmlDocument,
            },
            wait = new
            {
                @for = "navigation",
                timeout = 250,
                waitUntil = "load",
            },
        };

        Stream generatedPdf = null;

        while (tryNumber < 1)
        {
            var request = new RestRequest("https://yakpdf.p.rapidapi.com/pdf", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("X-RapidAPI-Key", getApiKey(tryNumber));
            request.AddHeader("X-RapidAPI-Host", "yakpdf.p.rapidapi.com");
            request.AddParameter("application/json", JsonSerializer.Serialize(requestBody), ParameterType.RequestBody);

            var response = await new RestClient().ExecuteAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                generatedPdf = new MemoryStream(response.RawBytes);

                return generatedPdf;
            }
            else
            {
                tryNumber++;
            }
        }
        throw new Exception("Não foi possível gerar o PDF");
    }

    private static string getApiKey(int tryNumber)
    {
        var apiKeys = new string[] { "221c10082bmshbf371bcfe5651b1p1fd52djsncdd5751bf22e" };
        return apiKeys[tryNumber];
    }
}