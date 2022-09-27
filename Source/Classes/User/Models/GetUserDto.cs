class GetUserDto
{
    public int id_usuario { get; set; }
    public string nome_completo { get; set; }
    public string email { get; set; }
    public string tipo { get; set; }
    public bool status { get; set; }

    public GetUserDto()
    {
    }

    public GetUserDto(int id_usuario, string nome_completo, string email, string tipo, bool status)
    {
        this.id_usuario = id_usuario;
        this.nome_completo = nome_completo;
        this.email = email;
        this.tipo = tipo;
        this.status = status;
    }
}