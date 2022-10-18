class GetVehicleByClientDto
{
    // Adaptado para o Management Stage.

    public int id_veiculo { get; set; }
    public string marca { get; set; }
    public string modelo { get; set; }
    public string ano { get; set; }
    public string uso { get; set; }
    public string placa { get; set; }

    public GetVehicleByClientDto(int id_veiculo, string marca, string modelo, string ano, string uso, string placa)
    {
        this.id_veiculo = id_veiculo;
        this.marca = marca;
        this.modelo = modelo;
        this.ano = ano;
        this.uso = uso;
        this.placa = placa;
    }

    public GetVehicleByClientDto()
    {
        // Default constructor
        this.id_veiculo = 0;
        this.marca = "any_brand";
        this.modelo = "any_model";
        this.ano = "any_year";
        this.uso = "any_use";
        this.placa = "any_plate";
    }
}