using tsb.mininal.policy.engine.Utils;

public class Ocorrencia
{
    public int id_ocorrencia { get; set; }
    public string data { get; set; }
    public string local { get; set; }
    public string UF { get; set; }
    
    public string municipio { get; set; }
    public string descricao { get; set; }
    public string tipo { get; set; }
    public string? documento { get; set; } // Base64 file? Binary? We still don't know.
    public int id_veiculo { get; set; }
    public int id_cliente { get; set; }
    public int? id_terceirizado { get; set; }
    public string status { get; set; }

    public Ocorrencia()
    {
        id_ocorrencia = 0;
        data = DateFormatter.FormatDate();
        local = "any_place";
        UF = "any_uf";
        municipio = "any_city";
        descricao = "any_description";
        tipo = "any_type";
        documento = "any_document_binary";
        id_veiculo = 0;
        id_cliente = 0;
        status = "any_status";
    }
    public Ocorrencia(string date, string place, string UF, string city, string description, string type, string? document, int idveiculo, int idcliente, int? idterceirizado, string status)
    {

        this.data = date;
        this.local = place;
        this.UF = UF;
        this.municipio = city;
        this.descricao = description;
        this.tipo = type;
        this.documento = document;
        this.id_veiculo = idveiculo;
        this.id_cliente = idcliente;
        this.id_terceirizado = idterceirizado;
        this.status = status;
    }
}