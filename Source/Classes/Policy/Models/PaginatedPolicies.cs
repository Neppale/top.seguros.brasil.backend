class PaginatedPolicies
{
    public GetPolicyDto[] policies { get; set; }
    public int totalPages { get; set; }

    public PaginatedPolicies(GetPolicyDto[] policies, int totalPages)
    {
        this.policies = policies;
        this.totalPages = totalPages;
    }

    public PaginatedPolicies()
    {
    }
}