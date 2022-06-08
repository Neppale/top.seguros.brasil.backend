public class Usuario
{
    public int id_usuario { get; set; }
    public string fullName { get; set; }
    public string email { get; set; }
    public string type { get; set; }

    public string password;
    public bool status { get; set; }

    public Usuario()
    {
        id_usuario = 0;
        fullName = "any_fullName";
        email = "any_email";
        type = "any_tipo";
        password = "any_password";
        status = true;

    }

    public Usuario(string fullName, string email, string type, string password, bool status)
    {
        this.fullName = fullName;
        this.email = email;
        this.type = type;
        this.password = password;
        this.status = status;
    }
}