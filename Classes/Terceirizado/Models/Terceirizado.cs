public class Terceirizado
{
  public int id_terceirizado { get; set; }
  public string nome { get; set; }
  public string funcao { get; set; }
  public string cnpj { get; set; }
  public string telefone { get; set; }

  public double valor { get; set; }
  public bool status { get; set; }

  public Terceirizado(string nome, string funcao, string cnpj, string telefone, double valor, bool status)
  {
    this.nome = nome;
    this.funcao = funcao;
    this.cnpj = cnpj;
    this.telefone = telefone;
    this.valor = valor;
    this.status = status;
  }

  public Terceirizado()
  {
  }
}