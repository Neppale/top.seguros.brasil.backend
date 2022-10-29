class PaginatedCoverages
{
    public Cobertura[] coverages { get; set; }
    public int totalPages { get; set; }

    public PaginatedCoverages(Cobertura[] coverages, int totalPages)
    {
        this.coverages = coverages;
        this.totalPages = totalPages;
    }

    public PaginatedCoverages()
    {
    }
}