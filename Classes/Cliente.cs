using tsb.mininal.policy.engine.Utils;
using Dapper;
using Microsoft.Data.SqlClient;
public class Cliente : ModeloClasses<Cliente>
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

  /** <summary> Esta função retorna todos os clientes no banco de dados. </summary>**/
  public IEnumerable<Cliente> Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    var data = connectionString.Query<Cliente>("SELECT * from Clientes");

    return data;
  }

  /** <summary> Esta função retorna um cliente em específico no banco de daods. </summary>**/
  public IEnumerable<Cliente> Get(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.Query<Cliente>($"SELECT * from Clientes WHERE id_cliente={id}");

    if (data.Count() == 0) throw new BadHttpRequestException("Cliente não encontrado.", statusCode: 404);

    return data;
  }


  /** <summary> Esta função insere um cliente no banco de dados. </summary>**/
  public IResult Insert(Cliente cliente, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    try
    {
      // Verificando se alguma das propriedades do cliente é nula.
      //TODO: Telefone2 pode ser nulo. Precisa ser ignorado por essa verificação.
      bool isValid = NullPropertyValidator.Validate(cliente);
      if (!isValid) return Results.BadRequest("Há um campo inválido na sua requisição.");

      // Verificação de CPF
      bool cpfIsValid = CpfValidator.Validate(cliente.cpf);
      if (!cpfIsValid) return Results.BadRequest("O CPF informado é inválido.");

      var data = connectionString.Query<Cliente>($"INSERT INTO Clientes (email, senha, nome_completo, cpf, cnh, cep, data_nascimento, telefone1, telefone2, status) VALUES ('{cliente.email}', '{cliente.senha}', '{cliente.nome_completo}', '{cliente.cpf}', '{cliente.cnh}', '{cliente.cep}', '{cliente.data_nascimento}', '{cliente.telefone1}', '{cliente.telefone2}', '{cliente.status}')");

      return Results.StatusCode(201);
    }
    catch (BadHttpRequestException)
    {
      //TODO: Exception Handler para mostrar o erro/statusCode correto com base na mensagem enviada pelo SQL server.
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }

  /** <summary> Esta função altera um cliente no banco de dados. </summary>**/
  public IResult Update(int id, Cliente cliente, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se alguma das propriedades do cliente é nula.
    bool isValid = NullPropertyValidator.Validate(cliente);
    if (!isValid) return Results.BadRequest("Há um campo inválido na sua requisição.");


    try
    {

      var data = connectionString.Query($"UPDATE Clientes SET email = '{cliente.email}', senha = '{cliente.senha}', nome_completo = '{cliente.nome_completo}', cnh = '{cliente.cnh}', cep = '{cliente.cep}', data_nascimento = '{cliente.data_nascimento}', telefone1 = '{cliente.telefone1}', telefone2 = '{cliente.telefone2}', status = '{cliente.status}' WHERE id_cliente = {id}");

      return Results.Ok();
    }
    catch (BadHttpRequestException)
    {
      //TODO: Exception Handler para mostrar o erro/statusCode correto com base na mensagem enviada pelo SQL server.
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }

}