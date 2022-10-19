class VehiclePrice
{
    public string Valor { get; set; }
    public string Marca { get; set; }
    public string Modelo { get; set; }
    public int AnoModelo { get; set; }
    public string Combustivel { get; set; }
    public string CodigoFipe { get; set; }
    public string MesReferencia { get; set; }
    public int TipoVeiculo { get; set; }
    public string SiglaCombustivel { get; set; }

    public VehiclePrice(string Valor, string Marca, string Modelo, int AnoModelo, string Combustivel, string CodigoFipe, string MesReferencia, int TipoVeiculo, string SiglaCombustivel)
    {
        this.Valor = Valor;
        this.Marca = Marca;
        this.Modelo = Modelo;
        this.AnoModelo = AnoModelo;
        this.Combustivel = Combustivel;
        this.CodigoFipe = CodigoFipe;
        this.MesReferencia = MesReferencia;
        this.TipoVeiculo = TipoVeiculo;
        this.SiglaCombustivel = SiglaCombustivel;
    }

    public VehiclePrice()
    {
        // Default constructor
        this.Valor = "any_value";
        this.Marca = "any_brand";
        this.Modelo = "any_model";
        this.AnoModelo = 0;
        this.Combustivel = "any_fuel";
        this.CodigoFipe = "any_fipe_code";
        this.MesReferencia = "any_reference_month";
        this.TipoVeiculo = 0;
        this.SiglaCombustivel = "any_fuel_initials";
    }
}