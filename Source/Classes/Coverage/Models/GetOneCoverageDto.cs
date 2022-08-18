class GetOneCoverageDto
{
  public int id_cobertura { get; set; }
  public string nome { get; set; }
  public string descricao { get; set; }
  public string valor { get; set; }
  public decimal taxa_indenizacao { get; set; }

  public GetOneCoverageDto(int id_cobertura, string nome, string descricao, string valor, decimal taxa_indenizacao)
  {
    this.id_cobertura = id_cobertura;
    this.nome = nome;
    this.descricao = descricao;
    this.valor = valor;
    this.taxa_indenizacao = taxa_indenizacao;
  }

  public GetOneCoverageDto()
  {
  }
}