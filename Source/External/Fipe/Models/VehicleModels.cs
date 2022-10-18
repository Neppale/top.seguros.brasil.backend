class VehicleModels
{
    public VehicleModel[] modelos { get; set; }
    public VehicleYears[] anos { get; set; }

    public VehicleModels(VehicleModel[] modelos, VehicleYears[] anos)
    {
        this.modelos = modelos;
        this.anos = anos;
    }

    public VehicleModels()
    {
        // Default constructor
        this.modelos = new VehicleModel[0];
        this.anos = new VehicleYears[0];
    }
}

class VehicleModel
{
    public int codigo { get; set; }
    public string nome { get; set; }

    public VehicleModel(int codigo, string nome)
    {
        this.codigo = codigo;
        this.nome = nome;
    }

    public VehicleModel()
    {
        // Default constructor
        this.codigo = 0;
        this.nome = "any_name";
    }
}
