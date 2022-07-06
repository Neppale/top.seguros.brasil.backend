static class ApoliceDocumentGenerator
{
  /**<summary> Esta função gera um arquivo pdf em apólice. O retorno é o diretório do documento no sistema. </summary>**/
  public static async Task<string> Generate(Apolice apolice, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    var usuario = connectionString.QueryFirstOrDefault("SELECT nome_completo, id_usuario FROM Usuarios WHERE id_usuario = @Id", new { Id = apolice.id_usuario });
    var cliente = connectionString.QueryFirstOrDefault("SELECT nome_completo, cpf, cep FROM Clientes WHERE id_cliente = @Id", new { Id = apolice.id_cliente });
    var veiculo = connectionString.QueryFirstOrDefault("SELECT placa, marca, modelo, ano, uso FROM Veiculos WHERE id_veiculo = @Id", new { Id = apolice.id_veiculo });
    var cobertura = connectionString.QueryFirstOrDefault("SELECT valor, descricao, taxa_indenizacao FROM Coberturas WHERE id_cobertura = @Id", new { Id = apolice.id_cobertura });
    var localizacao = await GetCepInfo.Get(cliente.cep);
    localizacao = await localizacao.ReadAsStringAsync();
    decimal veiculoPreco = await VehiclePriceFinder.Find(veiculo.marca, veiculo.modelo, veiculo.ano);

    string documentoHTML = await File.ReadAllTextAsync("Utils/Tools/Generators/ApoliceDocumentGenerator/Files/ApoliceDocument.html");

    // Alterando dados da apólice no documento.
    documentoHTML = documentoHTML.Replace("{{DATAHOJE}}", DateTime.Now.ToString("dd/MM/yyyy"));
    documentoHTML = documentoHTML.Replace("{{IDAPOLICE}}", apolice.id_apolice.ToString());
    documentoHTML = documentoHTML.Replace("{{DATAINICIAL}}", apolice.data_inicio.ToString().Substring(0, 10));
    documentoHTML = documentoHTML.Replace("{{DATAFINAL}}", apolice.data_fim.Substring(0, 10));

    // Alterando dados do usuário no documento.
    documentoHTML = documentoHTML.Replace("{{NOMEUSUARIO}}", usuario.nome_completo);
    documentoHTML = documentoHTML.Replace("{{IDUSUARIO}}", usuario.id_usuario.ToString());

    // Alterando dados do cliente no documento.
    documentoHTML = documentoHTML.Replace("{{NOMECLIENTE}}", cliente.nome_completo);
    documentoHTML = documentoHTML.Replace("{{CPFCLIENTE}}", cliente.cpf);
    documentoHTML = documentoHTML.Replace("{{CEPCLIENTE}}", cliente.cep);
    Regex regex = new Regex("\\\"logradouro\\\": \\\"[A-Za-z çáéíóúãñ]+\\\"");
    MatchCollection matches = regex.Matches(localizacao);
    string logradouro = matches[0].ToString().Substring(14);
    logradouro = logradouro.Replace("\"", "");
    logradouro = logradouro.Replace("\\", "");

    // Encontrando bairro do cliente.
    regex = new Regex("\\\"bairro\\\": \\\"[A-Za-z çáéíóúãñ]+\\\"");
    matches = regex.Matches(localizacao);
    string bairro = matches[0].ToString().Substring(11);
    bairro = bairro.Replace("\"", "");
    bairro = bairro.Replace("\\", "");

    // Encontrando estado do cliente.
    regex = new Regex("\\\"localidade\\\": \\\"[A-Za-z çáéíóúãñ]+\\\"");
    matches = regex.Matches(localizacao);
    string cidade = matches[0].ToString().Substring(15);
    cidade = cidade.Replace("\"", "");
    cidade = cidade.Replace("\\", "");
    string endereco = logradouro + ", " + bairro + ", " + cidade;

    // Encontrando UF do cliente.
    regex = new Regex("\\\"uf\\\": \\\"[A-Z]+\\\"");
    matches = regex.Matches(localizacao);
    string uf = matches[0].ToString().Substring(7);
    uf = uf.Replace("\"", "");
    uf = uf.Replace("\\", "");

    documentoHTML = documentoHTML.Replace("{{ENDERECOCLIENTE}}", endereco);
    documentoHTML = documentoHTML.Replace("{{UFCLIENTE}}", uf);

    // Alterando dados do veículo no documento.
    documentoHTML = documentoHTML.Replace("{{MARCAVEICULO}}", veiculo.marca);
    documentoHTML = documentoHTML.Replace("{{MODELOVEICULO}}", veiculo.modelo.Replace(@"\", ""));
    documentoHTML = documentoHTML.Replace("{{PLACAVEICULO}}", veiculo.placa);
    documentoHTML = documentoHTML.Replace("{{COMBUSTIVELVEICULO}}", veiculo.ano.Substring(veiculo.ano.IndexOf(" ") + 1));
    documentoHTML = documentoHTML.Replace("{{ANOVEICULO}}", veiculo.ano.Substring(0, veiculo.ano.IndexOf(" ")));
    documentoHTML = documentoHTML.Replace("{{USOVEICULO}}", veiculo.uso);

    // Alterando dados da cobertura no documento.
    documentoHTML = documentoHTML.Replace("{{DESCRICAOCOBERTURA}}", cobertura.descricao);
    documentoHTML = documentoHTML.Replace("{{COBERTURAVALOR}}", cobertura.valor.ToString());
    documentoHTML = documentoHTML.Replace("{{TAXAINDENIZACAOCOBERTURA}}", cobertura.taxa_indenizacao.ToString());

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