public class Cobertura
{
  public int id_cobertura { get; set; }
  public string nome { get; set; }
  public string descricao { get; set; }
  public string valor { get; set; }
  public bool status { get; set; }

  public Cobertura(string nome, string descricao, string valor, bool status)
  {
    this.nome = nome;
    this.descricao = descricao;
    this.valor = valor;
    this.status = status;
  }

  public Cobertura()
  {
  }
}