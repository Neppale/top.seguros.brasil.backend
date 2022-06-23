using Dapper;
using Microsoft.Data.SqlClient;
using tsb.mininal.policy.engine.Utils;
using DocumentValidator;
public static class InsertTerceirizadoService
{
  /** <summary> Esta função insere uma Terceirizado no banco de dados. </summary>**/
  public static IResult Insert(Terceirizado terceirizado, string dbConnectionString)
  {
    SqlConnection connectionString = new SqlConnection(dbConnectionString);

    // Verificando se alguma das propriedades do Terceirizado é nula ou vazia.
    bool hasValidProperties = NullPropertyValidator.Validate(terceirizado);
    if (!hasValidProperties) return Results.BadRequest("Há um campo inválido na sua requisição.");

    // Validando CNPJ
    bool cnpjIsValid = CnpjValidation.Validate(terceirizado.cnpj);
    cnpjIsValid = StringFormatValidator.ValidateCNPJ(terceirizado.cnpj);
    if (!cnpjIsValid) return Results.BadRequest("O CNPJ informado é inválido ou está mal formatado. Lembre-se de que o CNPJ deve estar no formato: 99.999.999/9999-99.");

    // Verificando se o CNPJ já existe no banco de dados.
    bool cnpjExists = connectionString.QueryFirstOrDefault<bool>("SELECT CASE WHEN EXISTS (SELECT cnpj FROM Terceirizados WHERE cnpj = @Cnpj) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END", new { Cnpj = terceirizado.cnpj });
    if (cnpjExists) return Results.Conflict("O CNPJ informado já está cadastrado.");

    // Verificando se o telefone está formatado corretamente.
    bool telefoneIsValid = StringFormatValidator.ValidateTelefone(terceirizado.telefone);
    if (!telefoneIsValid) return Results.BadRequest("O telefone informado está mal formatado. Lembre-se de que o telefone deve estar no formato: (99) 99999-9999.");

    // Verificando se o telefone já existe no banco de dados.
    bool telefoneExists = connectionString.QueryFirstOrDefault<bool>("SELECT telefone FROM Terceirizados WHERE telefone = @Telefone", new { Telefone = terceirizado.telefone });
    if (telefoneExists) return Results.Conflict("O telefone informado já está cadastrado.");

    try
    {
      connectionString.Query("INSERT INTO Terceirizados (nome, funcao, cnpj, telefone, valor) VALUES (@Nome, @Funcao, @Cnpj, @Telefone, @Valor)", new { Nome = terceirizado.nome, Funcao = terceirizado.funcao, Cnpj = terceirizado.cnpj, Telefone = terceirizado.telefone, Valor = terceirizado.valor });

      // Pegando o ID do Terceirizado que acabou de ser inserido.
      int createdTerceirizadoId = connectionString.QueryFirstOrDefault<int>("SELECT id_terceirizado FROM Terceirizados WHERE cnpj = @Cnpj", new { Cnpj = terceirizado.cnpj });

      return Results.Created($"/terceirizado/{createdTerceirizadoId}", new { id_terceirizado = createdTerceirizadoId });
    }
    catch (SystemException)
    {
      return Results.BadRequest("Requisição feita incorretamente. Confira todos os campos e tente novamente.");
    }

  }
}
