class PaginatedPoliciesByUser
{
    public GetPolicyByUserDto[] policies { get; set; }
    public int totalPages { get; set; }

    public PaginatedPoliciesByUser(GetPolicyByUserDto[] policies, int totalPages)
    {
        this.policies = policies;
        this.totalPages = totalPages;
    }

    public PaginatedPoliciesByUser()
    {
    }
}