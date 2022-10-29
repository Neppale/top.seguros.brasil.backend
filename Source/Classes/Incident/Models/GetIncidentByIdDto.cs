class GetIncidentByIdDto
{
    public int id_ocorrencia { get; set; }
    public string data { get; set; }
    public string local { get; set; }
    public string UF { get; set; }
    public string municipio { get; set; }
    public string descricao { get; set; }
    public string tipo { get; set; }
    public int id_veiculo { get; set; }
    public int id_cliente { get; set; }
    public int id_terceirizado { get; set; }
    public string status { get; set; }

    public GetIncidentByIdDto(int id_ocorrencia, string data, string local, string UF, string municipio, string descricao, string tipo, int id_veiculo, int id_cliente, int id_terceirizado, string status)
    {
        this.id_ocorrencia = id_ocorrencia;
        this.data = data;
        this.local = local;
        this.UF = UF;
        this.municipio = municipio;
        this.descricao = descricao;
        this.tipo = tipo;
        this.id_veiculo = id_veiculo;
        this.id_cliente = id_cliente;
        this.id_terceirizado = id_terceirizado;
        this.status = status;
    }

    public GetIncidentByIdDto()
    {
        // Default constructor
        this.id_ocorrencia = 0;
        this.data = "";
        this.local = "";
        this.UF = "";
        this.municipio = "";
        this.descricao = "";
        this.tipo = "";
        this.id_veiculo = 0;
        this.id_cliente = 0;
        this.id_terceirizado = 0;
        this.status = "";
    }
}