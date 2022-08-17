class GetOneClientDto
{
    public int id_cliente { get; set; }
    public string nome_completo { get; set; }
    public string email { get; set; }
    public string cpf { get; set; }
    public string cnh { get; set; }
    public string cep { get; set; }
    public string data_nascimento { get; set; }
    public string telefone1 { get; set; }
    public string telefone2 { get; set; }

    public GetOneClientDto(int id_cliente, string nome_completo, string email, string cpf, string cnh, string cep, string data_nascimento, string telefone1, string telefone2)
    {
        this.id_cliente = id_cliente;
        this.nome_completo = nome_completo;
        this.email = email;
        this.cpf = cpf;
        this.cnh = cnh;
        this.cep = cep;
        this.data_nascimento = data_nascimento;
        this.telefone1 = telefone1;
        this.telefone2 = telefone2;
    }
}