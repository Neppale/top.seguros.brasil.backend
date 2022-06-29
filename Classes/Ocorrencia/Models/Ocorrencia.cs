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

  public Ocorrencia(string date, string place, string UF, string city, string description, string type, string? document, string? documentType, int idveiculo, int idcliente, int? idterceirizado, string status)
  {

    this.data = date;
    this.local = place;
    this.UF = UF;
    this.municipio = city;
    this.descricao = description;
    this.tipo = type;
    this.documento = document;
    this.tipoDocumento = documentType;
    this.id_veiculo = idveiculo;
    this.id_cliente = idcliente;
    this.id_terceirizado = idterceirizado;
    this.status = status;
  }

  public Ocorrencia()
  {
  }
}