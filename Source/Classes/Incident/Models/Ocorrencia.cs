public class Ocorrencia
{
    public int id_ocorrencia { get; set; }
    public string data { get; set; }
    public string local { get; set; }
    public string UF { get; set; }
    public string municipio { get; set; }
    public string descricao { get; set; }
    public string tipo { get; set; }
    public string? documento { get; set; }
    public string? tipoDocumento { get; set; }
    public int id_veiculo { get; set; }
    public int id_cliente { get; set; }
    public int? id_terceirizado { get; set; }
    public string status { get; set; }

    public Ocorrencia(string data, string local, string UF, string municipio, string descricao, string tipo, string? documento, string? tipoDocumento, int id_veiculo, int id_cliente, int? id_terceirizado, string status)
    {
        this.data = data;
        this.local = local;
        this.UF = UF;
        this.municipio = municipio;
        this.descricao = descricao;
        this.tipo = tipo;
        this.documento = documento;
        this.tipoDocumento = tipoDocumento;
        this.id_veiculo = id_veiculo;
        this.id_cliente = id_cliente;
        this.id_terceirizado = id_terceirizado;
        this.status = status;
    }

    public Ocorrencia()
    {
    }
}