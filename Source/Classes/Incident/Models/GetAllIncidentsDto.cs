class GetAllIncidentsDto
{
    // Adaptado para o Management Stage.
    public int id_ocorrencia { get; set; }
    public string nome { get; set; }
    public string tipo { get; set; }
    public string data { get; set; }
    public string status { get; set; }

    public GetAllIncidentsDto(int id_ocorrencia, string nome, string tipo, string data, string status)
    {
        this.id_ocorrencia = id_ocorrencia;
        this.nome = nome;
        this.tipo = tipo;
        this.data = data;
        this.status = status;
    }

    GetAllIncidentsDto()
    {
        // Default constructor
        this.id_ocorrencia = 0;
        this.nome = "any_name";
        this.tipo = "any_type";
        this.data = "any_date";
        this.status = "any_status";
    }
}