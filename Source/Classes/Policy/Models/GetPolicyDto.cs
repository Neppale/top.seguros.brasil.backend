public class GetPolicyDto
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

  public GetPolicyDto(string data_inicio, string data_fim, decimal premio, decimal indenizacao, int id_cobertura, int id_usuario, int id_cliente, int id_veiculo, string status)
  {
    this.data_inicio = data_inicio;
    this.data_fim = data_fim;
    this.premio = premio;
    this.indenizacao = indenizacao;
    this.id_cobertura = id_cobertura;
    this.id_usuario = id_usuario;
    this.id_cliente = id_cliente;
    this.id_veiculo = id_veiculo;
    this.status = status;
  }

  public GetPolicyDto()
  {
  }
}