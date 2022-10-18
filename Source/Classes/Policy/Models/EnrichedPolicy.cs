class EnrichedPolicy
{

    public int id_apolice { get; set; }
    public string data_inicio { get; set; }
    public string data_fim { get; set; }
    public decimal premio { get; set; }
    public decimal indenizacao { get; set; }
    public Cobertura cobertura { get; set; }
    public GetUserDto usuario { get; set; }
    public GetClientDto cliente { get; set; }
    public Veiculo veiculo { get; set; }
    public string status { get; set; }

    public EnrichedPolicy(int id_apolice, string data_inicio, string data_fim, decimal premio, decimal indenizacao, Cobertura cobertura, GetUserDto usuario, GetClientDto cliente, Veiculo veiculo, string status)
    {
        this.id_apolice = id_apolice;
        this.data_inicio = data_inicio;
        this.data_fim = data_fim;
        this.premio = premio;
        this.indenizacao = indenizacao;
        this.cobertura = cobertura;
        this.usuario = usuario;
        this.cliente = cliente;
        this.veiculo = veiculo;
        this.status = status;
    }

    public EnrichedPolicy()
    {
        // Default constructor
        this.id_apolice = 0;
        this.data_inicio = "any_date";
        this.data_fim = "any_date";
        this.premio = 0;
        this.indenizacao = 0;
        this.cobertura = new Cobertura();
        this.usuario = new GetUserDto();
        this.cliente = new GetClientDto();
        this.veiculo = new Veiculo();
        this.status = "any_status";
    }   
}