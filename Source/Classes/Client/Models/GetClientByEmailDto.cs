class GetClientByEmailDto
{
    int id_cliente { get; set; }
    string nome_completo { get; set; }
    string email { get; set; }
    string data_nascimento { get; set; }
    string telefone1 { get; set; }
    string telefone2 { get; set; }
    string cnh { get; set; }
    string cep { get; set; }
    string cpf { get; set; }

    public GetClientByEmailDto(int id_cliente, string nome_completo, string email, string data_nascimento, string telefone1, string telefone2, string cnh, string cep, string cpf)
    {
        this.id_cliente = id_cliente;
        this.nome_completo = nome_completo;
        this.email = email;
        this.data_nascimento = data_nascimento;
        this.telefone1 = telefone1;
        this.telefone2 = telefone2;
        this.cnh = cnh;
        this.cep = cep;
        this.cpf = cpf;
    }
}