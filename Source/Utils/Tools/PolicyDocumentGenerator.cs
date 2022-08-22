static class PolicyDocumentGenerator
{
  /**<summary> Esta função gera um arquivo pdf em apólice. O retorno é o diretório do documento no sistema. </summary>**/
  public static async Task<string> Generate(Apolice apolice, SqlConnection connectionString)
  {
    var user = GetOneUserRepository.Get(id: apolice.id_usuario, connectionString: connectionString);
    var client = GetClientByIdRepository.Get(id: apolice.id_cliente, connectionString: connectionString);
    var vehicle = GetOneVehicleRepository.Get(id: apolice.id_veiculo, connectionString: connectionString);
    var coverage = GetOneCoverageRepository.Get(id: apolice.id_cobertura, connectionString: connectionString);
    HttpContent localizationResponse = await GetCepInfo.Get(client.cep);
    string localizationString = await localizationResponse.ReadAsStringAsync();
    decimal veiculoPreco = await VehiclePriceFinder.Find(vehicle.marca, vehicle.modelo, vehicle.ano);

    string documentoHTML = await File.ReadAllTextAsync("Source/Utils/Tools/Files/PolicyDocument.html");

    // Alterando dados da apólice no documento.
    documentoHTML = documentoHTML.Replace("{{DATAHOJE}}", DateTime.Now.ToString("dd/MM/yyyy"));
    documentoHTML = documentoHTML.Replace("{{IDAPOLICE}}", apolice.id_apolice.ToString());
    documentoHTML = documentoHTML.Replace("{{DATAINICIAL}}", apolice.data_inicio.ToString().Substring(0, 10));
    documentoHTML = documentoHTML.Replace("{{DATAFINAL}}", apolice.data_fim.Substring(0, 10));

    // Alterando dados do usuário no documento.
    documentoHTML = documentoHTML.Replace("{{NOMEUSUARIO}}", user.nome_completo);
    documentoHTML = documentoHTML.Replace("{{IDUSUARIO}}", user.id_usuario.ToString());

    // Alterando dados do cliente no documento.
    documentoHTML = documentoHTML.Replace("{{NOMECLIENTE}}", client.nome_completo);
    documentoHTML = documentoHTML.Replace("{{CPFCLIENTE}}", client.cpf);
    documentoHTML = documentoHTML.Replace("{{CEPCLIENTE}}", client.cep);
    Regex regex = new Regex("\\\"logradouro\\\": \\\"[A-Za-z çáéíóúãñ]+\\\"");
    MatchCollection matches = regex.Matches(localizationString);
    string logradouro = matches[0].ToString().Substring(14);
    logradouro = logradouro.Replace("\"", "");
    logradouro = logradouro.Replace("\\", "");

    // Encontrando bairro do cliente.
    regex = new Regex("\\\"bairro\\\": \\\"[A-Za-z çáéíóúãñ]+\\\"");
    matches = regex.Matches(localizationString);
    string bairro = matches[0].ToString().Substring(11);
    bairro = bairro.Replace("\"", "");
    bairro = bairro.Replace("\\", "");

    // Encontrando estado do cliente.
    regex = new Regex("\\\"localidade\\\": \\\"[A-Za-z çáéíóúãñ]+\\\"");
    matches = regex.Matches(localizationString);
    string cidade = matches[0].ToString().Substring(15);
    cidade = cidade.Replace("\"", "");
    cidade = cidade.Replace("\\", "");
    string endereco = logradouro + ", " + bairro + ", " + cidade;

    // Encontrando UF do cliente.
    regex = new Regex("\\\"uf\\\": \\\"[A-Z]+\\\"");
    matches = regex.Matches(localizationString);
    string uf = matches[0].ToString().Substring(7);
    uf = uf.Replace("\"", "");
    uf = uf.Replace("\\", "");

    documentoHTML = documentoHTML.Replace("{{ENDERECOCLIENTE}}", endereco);
    documentoHTML = documentoHTML.Replace("{{UFCLIENTE}}", uf);

    // Alterando dados do veículo no documento.
    documentoHTML = documentoHTML.Replace("{{MARCAVEICULO}}", vehicle.marca);
    documentoHTML = documentoHTML.Replace("{{MODELOVEICULO}}", vehicle.modelo.Replace(@"\", ""));
    documentoHTML = documentoHTML.Replace("{{PLACAVEICULO}}", vehicle.placa);
    documentoHTML = documentoHTML.Replace("{{COMBUSTIVELVEICULO}}", vehicle.ano.Substring(vehicle.ano.IndexOf(" ") + 1));
    documentoHTML = documentoHTML.Replace("{{ANOVEICULO}}", vehicle.ano.Substring(0, vehicle.ano.IndexOf(" ")));
    documentoHTML = documentoHTML.Replace("{{USOVEICULO}}", vehicle.uso);

    // Alterando dados da cobertura no documento.
    documentoHTML = documentoHTML.Replace("{{DESCRICAOCOBERTURA}}", coverage.descricao);
    documentoHTML = documentoHTML.Replace("{{COBERTURAVALOR}}", coverage.valor.ToString());
    documentoHTML = documentoHTML.Replace("{{TAXAINDENIZACAOCOBERTURA}}", coverage.taxa_indenizacao.ToString());

    // Dados finais do documento.
    documentoHTML = documentoHTML.Replace("{{VALORVEICULOFIPE}}", veiculoPreco.ToString());
    documentoHTML = documentoHTML.Replace("{{PREMIOAPOLICE}}", apolice.premio.ToString());
    documentoHTML = documentoHTML.Replace("{{INDENIZACAOAPOLICE}}", apolice.indenizacao.ToString());

    string temporaryDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Temp");

    string filePath = Path.Combine(temporaryDirectory, $"{DateTime.Now.ToString("yyyy-MM-dd")}-{Guid.NewGuid()}.pdf");
    var converter = new ConverterProperties();
    converter.SetBaseUri(documentoHTML);
    HtmlConverter.ConvertToPdf(documentoHTML, new FileStream(filePath, FileMode.Create), converter);

    return filePath;
  }


}