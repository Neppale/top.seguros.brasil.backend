static class GetUserReportRepository
{
    public static async Task<UserReport> Get(SqlConnection connectionString, int id)
    {
        var userPoliciesQuery = "SELECT * FROM Apolices WHERE id_usuario = @id AND status = 'Ativa'";
        var userPolicies = await connectionString.QueryAsync<GetPolicyDto>(userPoliciesQuery, new { id = id });

        decimal estimatedTotalGains = 0;

        var estimatedMonthlyGains = Math.Round(estimatedTotalGains / 12, 2);

        var userClients = new List<int>();
        foreach (var policy in userPolicies) userClients.Add(policy.id_cliente);

        var userIncidentsQuery = "SELECT * FROM Ocorrencias WHERE id_cliente IN @userClients AND status = 'Concluida' AND data BETWEEN @startDate AND @endDate";
        var userIncidents = await connectionString.QueryAsync<GetIncidentByIdDto>(userIncidentsQuery, new { userClients = userClients, startDate = DateTime.Now.AddMonths(-1), endDate = DateTime.Now });

        decimal estimatedMonthlyExpenses = 0;
        var outsourcedQuery = "SELECT * FROM Terceirizados WHERE id_terceirizado = @id_terceirizado";
        foreach (var incident in userIncidents)
        {
            if (incident.id_terceirizado != 0)
            {
                var outsourced = await connectionString.QueryFirstOrDefaultAsync<Terceirizado>(outsourcedQuery, new { id_terceirizado = incident.id_terceirizado });
                estimatedMonthlyExpenses += (decimal)outsourced.valor;
            }
        }

        var policyCountQuery = "SELECT COUNT(*) FROM Apolices WHERE id_usuario = @id";
        var policyCount = await connectionString.QueryFirstOrDefaultAsync<int>(policyCountQuery, new { id = id });

        var clientCount = userClients.Count;

        return new UserReport(Math.Round(estimatedTotalGains, 2), estimatedMonthlyGains, estimatedMonthlyExpenses, policyCount, clientCount);
    }
}
