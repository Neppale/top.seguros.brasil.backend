class GetAllVehicleDto
{
    // Adaptado para o Management Stage.
    public int id_veiculo { get; set; }
    public string marca { get; set; }
    public string modelo { get; set; }
    public string dono { get; set; }
    public string placa { get; set; }

    public GetAllVehicleDto(int id_veiculo, string marca, string modelo, string dono, string placa)
    {
        this.id_veiculo = id_veiculo;
        this.marca = marca;
        this.modelo = modelo;
        this.dono = dono;
        this.placa = placa;
    }

    public GetAllVehicleDto()
    {
        // Default constructor
        this.id_veiculo = 0;
        this.marca = "any_brand";
        this.modelo = "any_model";
        this.dono = "any_owner";
        this.placa = "any_plate";
    }
}