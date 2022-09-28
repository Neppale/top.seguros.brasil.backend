static class PolicyDocumentGenerator
{
    /**<summary> Esta função gera um arquivo pdf em apólice. O retorno é o diretório do documento no sistema. </summary>**/
    public static async Task<string> Generate(Apolice apolice, SqlConnection connectionString)
    {
        var user = await GetUserByIdRepository.Get(id: apolice.id_usuario, connectionString: connectionString);
        var client = await GetClientByIdRepository.Get(id: apolice.id_cliente, connectionString: connectionString);
        var vehicle = await GetVehicleByIdRepository.Get(id: apolice.id_veiculo, connectionString: connectionString);
        var coverage = await GetCoverageByIdRepository.Get(id: apolice.id_cobertura, connectionString: connectionString);
        var localization = await GetCepInfo.Get(client.cep);
        decimal veiculoPreco = await VehiclePriceFinder.Find(vehicle.marca, vehicle.modelo, vehicle.ano);

        string documentoHTML = await File.ReadAllTextAsync("./Source/Utils/Tools/Files/PolicyDocument.html");

        documentoHTML = FormatHtmlDocument(apolice, user, client, vehicle, coverage, localization, veiculoPreco, documentoHTML);

        try
        {
            string temporaryDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Temp");
            if (Directory.Exists(temporaryDirectory)) Console.WriteLine("[INFO] Diretório temporário existe.");


            string filePath = Path.Combine(temporaryDirectory, $"{DateTime.Now.ToString("yyyy-MM-dd")}-{Guid.NewGuid()}.pdf");
            var converter = new ConverterProperties();
            converter.SetBaseUri(documentoHTML);
            HtmlConverter.ConvertToPdf(documentoHTML, new FileStream(filePath, FileMode.Create), converter);

            return filePath;
        }
        catch (SystemException)
        {
            string rootDirectory = Directory.GetCurrentDirectory();
            string[] files = Directory.GetFiles(rootDirectory, "*.pdf");
            foreach (string file in files) File.Delete(file);

            string filePath = Path.Combine(rootDirectory, $"{DateTime.Now.ToString("yyyy-MM-dd")}-{Guid.NewGuid()}.pdf");
            var converter = new ConverterProperties();
            converter.SetBaseUri(documentoHTML);
            HtmlConverter.ConvertToPdf(documentoHTML, new FileStream(filePath, FileMode.Create), converter);

            return filePath;
        }
    }

    private static string FormatHtmlDocument(Apolice apolice, GetUserDto user, GetClientDto client, Veiculo vehicle, Cobertura coverage, CepInfo localization, decimal veiculoPreco, string documentoHTML)
    {
        documentoHTML = documentoHTML.Replace("{{DATAHOJE}}", DateTime.Now.ToString("dd/MM/yyyy"))
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
        return documentoHTML;
    }
}