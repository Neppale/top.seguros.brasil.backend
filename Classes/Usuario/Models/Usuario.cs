public class Usuario
{
  public int id_usuario { get; set; }
  public string nome_completo { get; set; }
  public string email { get; set; }
  public string tipo { get; set; }

  public string senha { get; set; }
  public bool status { get; set; }

  public Usuario()
  {
  }

  public Usuario(string fullName, string email, string type, string password, bool status)
  {
    this.nome_completo = fullName;
    this.email = email;
    this.tipo = type;
    this.senha = password;
    this.status = status;
  }
}