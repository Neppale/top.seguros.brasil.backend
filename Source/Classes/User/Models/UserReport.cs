class UserReport
{
    public decimal estimatedTotalGains { get; set; }
    public decimal estimatedMonthlyExpenses { get; set; }
    public decimal estimatedMonthlyGains { get; set; }
    public int policyCount { get; set; }
    public int clientCount { get; set; }

    public UserReport()
    {
        // Default constructor
        this.estimatedTotalGains = 0;
        this.estimatedMonthlyExpenses = 0;
        this.estimatedMonthlyGains = 0;
        this.policyCount = 0;
        this.clientCount = 0;
    }

    public UserReport(decimal estimatedTotalGains, decimal estimatedMonthlyGains, decimal estimatedMonthlyExpenses, int policyCount, int clientCount)
    {
        this.estimatedTotalGains = estimatedTotalGains;
        this.estimatedMonthlyExpenses = estimatedMonthlyExpenses;
        this.estimatedMonthlyGains = estimatedMonthlyGains;
        this.policyCount = policyCount;
        this.clientCount = clientCount;
    }
}