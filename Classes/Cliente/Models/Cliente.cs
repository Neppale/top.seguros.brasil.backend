public class Cliente
{
  public int id_cliente { get; set; }
  public string nome_completo { get; set; }
  public string email { get; set; }
  public string senha { get; set; }
  public string cpf { get; set; }
  public string cnh { get; set; }
  public string cep { get; set; }
  public string data_nascimento { get; set; }
  public string telefone1 { get; set; }
  public string? telefone2 { get; set; }
  public bool status { get; set; }

  public Cliente(string fullName, string email, string password, string cpf, string cnh, string cep, string birthdate, string phone1, string? phone2, bool status)
  {
    this.nome_completo = fullName;
    this.email = email;
    this.senha = password;
    this.cpf = cpf;
    this.cnh = cnh;
    this.cep = cep;
    this.data_nascimento = birthdate;
    this.telefone1 = phone1;
    this.telefone2 = phone2;
    this.status = status;
  }

  public Cliente()
  {
  }
}