class GetPolicyByUserDto
{

    // Adaptado para o Management Stage.

    public int id_apolice { get; set; }
    public string nome { get; set; }
    public string tipo { get; set; }
    public string veiculo { get; set; }
    public string status { get; set; }

    public GetPolicyByUserDto(int id_apolice, string nome, string tipo, string veiculo, string status)
    {
        this.id_apolice = id_apolice;
        this.nome = nome;
        this.tipo = tipo;
        this.veiculo = veiculo;
        this.status = status;
    }

    public GetPolicyByUserDto()
    {
        // Default constructor
        this.id_apolice = 0;
        this.nome = "any_name";
        this.tipo = "any_type";
        this.veiculo = "any_vehicle";
        this.status = "any_status";
    }
}