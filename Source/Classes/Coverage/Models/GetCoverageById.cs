class GetCoverageByIdDto
{
    public int id_cobertura { get; set; }
    public string nome { get; set; }
    public string descricao { get; set; }
    public decimal valor { get; set; }
    public decimal taxa_indenizacao { get; set; }

    public GetCoverageByIdDto(int id_cobertura, string nome, string descricao, decimal valor, decimal taxa_indenizacao)
    {
        this.id_cobertura = id_cobertura;
        this.nome = nome;
        this.descricao = descricao;
        this.valor = valor;
        this.taxa_indenizacao = taxa_indenizacao;
    }

    public GetCoverageByIdDto()
    {
        this.id_cobertura = 0;
        this.nome = "";
        this.descricao = "";
        this.valor = 0;
        this.taxa_indenizacao = 0;
    }
}