public static class ClientIsDeletedValidator
{
    public static bool Validate(string cpf, SqlConnection connectionString)
    {
        string storedStatus = connectionString.QueryFirstOrDefault<string>("SELECT status FROM Clientes WHERE cpf = @Cpf", new { Cpf = cpf });
        if (storedStatus == "false") return false;
        return true;
    }
}