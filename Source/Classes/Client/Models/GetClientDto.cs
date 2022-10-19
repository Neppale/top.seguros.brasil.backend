class GetClientDto
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

    public GetClientDto(int id_cliente, string nome_completo, string email, string cpf, string cnh, string cep, string data_nascimento, string telefone1, string telefone2, bool status)
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

    public GetClientDto()
    {
        // Default constructor
        this.id_cliente = 0;
        this.nome_completo = "any_name";
        this.email = "any_email";
        this.cpf = "any_cpf";
        this.cnh = "any_cnh";
        this.cep = "any_cep";
        this.data_nascimento = "any_birthdate";
        this.telefone1 = "any_phone1";
        this.telefone2 = "any_phone2";
    }
}