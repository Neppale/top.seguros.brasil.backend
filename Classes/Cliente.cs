using tsb.mininal.policy.engine.Utils;
using DocumentValidator;
using Dapper;
using Microsoft.Data.SqlClient;
public class Cliente
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
  public IResult Get(string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    var data = connectionString.Query<Cliente>("SELECT * from Clientes WHERE status= 'true'");

    return Results.Ok(data);
  }

  /** <summary> Esta função retorna um cliente em específico no banco de daods. </summary>**/
  public IResult Get(int id, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    var data = connectionString.QueryFirstOrDefault<Cliente>("SELECT * from Clientes WHERE id_cliente = @Id", new { Id = id });

    if (data == null) return Results.NotFound("Cliente não encontrado.");

    return Results.Ok(data);
  }


  /** <summary> Esta função insere um cliente no banco de dados. </summary>**/
  public IResult Insert(Cliente cliente, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    try
    {
      // Verificando se alguma das propriedades do cliente é nula ou vazia.
      //TODO: Telefone2 pode ser nulo. Precisa ser ignorado por essa verificação.
      bool hasValidProperties = NullPropertyValidator.Validate(cliente);
      if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

      // Verificação de CPF
      bool cpfIsValid = CpfValidation.Validate(cliente.cpf);
      if (!cpfIsValid) return Results.BadRequest("O CPF informado é inválido.");

      // Verificação de CNH
      bool cnhIsValid = CnhValidation.Validate(cliente.cnh);
      if (!cnhIsValid) return Results.BadRequest("O CPF informado é inválido.");

      // Verificação de CEP
      Task<bool> cepIsValid = CepValidator.Validate(cliente.cep);
      if (!cepIsValid.Result) return Results.BadRequest("O CEP informado é inválido.");

      // Criptografando a senha do cliente.
      cliente.senha = PasswordHasher.HashPassword(cliente.senha);

      var data = connectionString.Query<Cliente>("INSERT INTO Clientes (email, senha, nome_completo, cpf, cnh, cep, data_nascimento, telefone1, telefone2, status) VALUES (@Email, @Senha, @Nome, @Cpf, @Cnh, @Cep, @DataNascimento, @Telefone1, @Telefone2, @Status)",
        new { Email = cliente.email, Senha = cliente.senha, Nome = cliente.nome_completo, Cpf = cliente.cpf, Cnh = cliente.cnh, Cep = cliente.cep, DataNascimento = cliente.data_nascimento, Telefone1 = cliente.telefone1, Telefone2 = cliente.telefone2, Status = cliente.status });

      return Results.StatusCode(201);
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }

  /** <summary> Esta função altera um cliente no banco de dados. </summary>**/
  public IResult Update(int id, Cliente cliente, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se alguma das propriedades do cliente é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(cliente);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    // Verificação de CNH
    bool cnhIsValid = CnhValidation.Validate(cliente.cnh);
    if (!cnhIsValid) return Results.BadRequest("O CPF informado é inválido.");

    // Verificação de CEP
    Task<bool> cepIsValid = CepValidator.Validate(cliente.cep);
    if (!cepIsValid.Result) return Results.BadRequest("O CEP informado é inválido.");

    // Criptografando a senha do cliente.
    cliente.senha = PasswordHasher.HashPassword(cliente.senha);

    try
    {
      var data = connectionString.Query("UPDATE Clientes SET email = @Email, senha = @Senha, nome_completo = @Nome, cnh = @Cnh, cep = @Cep, data_nascimento = @DataNascimento, telefone1 = @Telefone1, telefone2 = @Telefone2, status = @Status WHERE id_cliente = @Id",
        new { Email = cliente.email, Senha = cliente.senha, Nome = cliente.nome_completo, Cnh = cliente.cnh, Cep = cliente.cep, DataNascimento = cliente.data_nascimento, Telefone1 = cliente.telefone1, Telefone2 = cliente.telefone2, Status = cliente.status, Id = id });

      return Results.Ok();
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }

  /** <summary> Esta função faz o login do cliente. </summary>**/
  public IResult Login(string email, string password, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);
    try
    {
      string hashPassword = connectionString.QueryFirstOrDefault<string>("SELECT senha FROM Clientes WHERE email = @Email", new { Email = email });

      if (hashPassword == null) return Results.BadRequest("E-mail ou senha inválidos.");

      // Verificando senha do cliente.
      bool isValid = PasswordHasher.Verify(hashPassword, password);
      if (!isValid) return Results.BadRequest("E-mail ou senha inválidos.");

      return Results.Ok();
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }
  }
}