class ClientLoginDto
{
    public string email { get; set; }
    public string senha { get; set; }

    public ClientLoginDto(string email, string senha)
    {
        this.email = email;
        this.senha = senha;
    }

    public ClientLoginDto()
    {
        // Default constructor
        this.email = "any_email";
        this.senha = "any_password";
    }
}