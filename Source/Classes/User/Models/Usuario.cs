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

    public Usuario(string nome_completo, string email, string senha, string tipo, bool status)
    {
        this.nome_completo = nome_completo;
        this.email = email;
        this.senha = senha;
        this.tipo = tipo;
        this.status = status;
    }
}