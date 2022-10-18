class VehicleBrands
{
    public string codigo { get; set; }
    public string nome { get; set; }

    public VehicleBrands(string codigo, string nome)
    {
        this.codigo = codigo;
        this.nome = nome;
    }

    public VehicleBrands()
    {
        // Default constructor
        this.codigo = "any_code";
        this.nome = "any_name";
    }
}