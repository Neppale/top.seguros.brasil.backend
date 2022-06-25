public class Apolice
{
  public int id_apolice { get; set; }
  public string data_inicio { get; set; }
  public string data_fim { get; set; }
  public decimal premio { get; set; }
  public decimal indenizacao { get; set; }
  public int id_cobertura { get; set; }
  public int id_usuario { get; set; }
  public int id_cliente { get; set; }
  public int id_veiculo { get; set; }
  public string status { get; set; }

  public Apolice(string startDate, string endDate, decimal premium, decimal indemnity, int idcobertura, int idusuario, int idcliente, int idveiculo, string status)
  {

    this.data_inicio = startDate;
    this.data_fim = endDate;
    this.premio = premium;
    this.indenizacao = indemnity;
    this.id_cobertura = idcobertura;
    this.id_usuario = idusuario;
    this.id_cliente = idcliente;
    this.id_veiculo = idveiculo;
    this.status = status;
  }

  public Apolice()
  {
  }
}