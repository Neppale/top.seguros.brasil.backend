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
        // Default constructor
        this.coverages = new Cobertura[0];
        this.totalPages = 0;
    }
}