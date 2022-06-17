public class Terceirizado
{
  public int id_terceirizado { get; set; }
  public string nome { get; set; }
  public string funcao { get; set; }
  public string cnpj { get; set; }
  public string telefone { get; set; }

  public double valor { get; set; }
  public bool status { get; set; }

  public Terceirizado(string fullName, string function, string cnpj, string phone, double price, bool status)
  {
    this.nome = fullName;
    this.funcao = function;
    this.cnpj = cnpj;
    this.telefone = phone;
    this.valor = price;
    this.status = status;
  }

  public Terceirizado()
  {
  }
}