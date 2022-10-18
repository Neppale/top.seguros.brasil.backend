class VehicleYears
{
    public string codigo { get; set; }
    public string nome { get; set; }

    public VehicleYears(string codigo, string nome)
    {
        this.codigo = codigo;
        this.nome = nome;
    }

    public VehicleYears()
    {
        // Default constructor
        this.codigo = "any_code";
        this.nome = "any_name";
    }
}