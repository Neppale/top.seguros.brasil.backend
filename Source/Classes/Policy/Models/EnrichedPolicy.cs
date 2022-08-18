class EnrichedPolicy
{

  public int id_apolice { get; set; }
  public string data_inicio { get; set; }
  public string data_fim { get; set; }
  public decimal premio { get; set; }
  public decimal indenizacao { get; set; }
  public Cobertura cobertura { get; set; }
  public GetUserDto usuario { get; set; }
  public GetOneClientDto cliente { get; set; }
  public Veiculo veiculo { get; set; }
  public string status { get; set; }

}