using Dapper;
using Microsoft.Data.SqlClient;
public class Cliente
{
    public int id_cliente { get; set; }
    public string nome_completo { get; set; }
    public string email { get; set; }
    public string password;
    public string cpf { get; set; }
    public string cnh { get; set; }
    public string cep { get; set; }
    public string data_nascimento { get; set; }
    public string telefone1 { get; set; }
    public string? telefone2 { get; set; }
    public bool status { get; set; }

    public Cliente()
    {
        id_cliente = 1;
        email = "any_email";
        nome_completo = "any_fullName";
        password = "any_password";
        cpf = "any_cpf";
        cnh = "any_cnh";
        cep = "any_cep";
        data_nascimento = "any_birthdate";
        telefone1 = "any_phone1";
        status = true;

    }

    public Cliente(string fullName, string email, string password, string cpf, string cnh, string cep, string birthdate, string phone1, string? phone2, bool status)
    {
        this.nome_completo = fullName;
        this.email = email;
        this.password = password;
        this.cpf = cpf;
        this.cnh = cnh;
        this.cep = cep;
        this.data_nascimento = birthdate;
        this.telefone1 = phone1;
        this.telefone2 = phone2;
        this.status = status;
    }

    // Esta função retorna todos os clientes do banco de dados.
    public IEnumerable<Cliente> GetCliente(string dbConnectionString)
    {
        SqlConnection connectionString = new SqlConnection(dbConnectionString);

        var data = connectionString.Query<Cliente>("SELECT * from Clientes");

        Console.WriteLine("[INFO] A request for all 'clientes' was made. The response is not a mock. :)");

        return data; 
    }

    // Esta função retorna apenas um cliente em específico.
    public IEnumerable<Cliente> GetCliente(int id, string dbConnectionString)
    {
        SqlConnection connectionString = new SqlConnection(dbConnectionString);
        var data = connectionString.Query<Cliente>($"SELECT * from Clientes WHERE id_cliente={id}");

        Console.WriteLine("[INFO] A request for a single 'cliente' was made. The response is not a mock. :)");

        if (data.Count() == 0) throw new BadHttpRequestException("Cliente não encontrado.", statusCode: 404);

        return data;
    }

}