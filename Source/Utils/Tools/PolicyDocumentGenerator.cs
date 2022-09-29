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

        string htmlDocument = await File.ReadAllTextAsync("./Source/Utils/Tools/Files/PolicyDocument.html");
        htmlDocument = FormatHtmlDocument(apolice, user, client, vehicle, coverage, localization, veiculoPreco, htmlDocument);

        var converter = new BasicConverter(new PdfTools());
        var pdfDocument = new HtmlToPdfDocument()
        {
            GlobalSettings = {
        ColorMode = ColorMode.Grayscale,
        Orientation = Orientation.Portrait,
        PaperSize = PaperKind.A4,
    },
            Objects = {
        new ObjectSettings() {
            PagesCount = true,
            HtmlContent = htmlDocument,
            WebSettings = { DefaultEncoding = "utf-8" },
        }
    }
        };
        byte[] pdfDocumentBytes = converter.Convert(pdfDocument);
        return new MemoryStream(pdfDocumentBytes);
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
}