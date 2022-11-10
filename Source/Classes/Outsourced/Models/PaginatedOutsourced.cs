class PaginatedOutsourced
{
    public Terceirizado[] outsourceds { get; set; }
    public int totalPages { get; set; }

    public PaginatedOutsourced(Terceirizado[] outsourceds, int totalPages)
    {
        this.outsourceds = outsourceds;
        this.totalPages = totalPages;
    }

    public PaginatedOutsourced()
    {
        // Default constructor
        this.outsourceds = new Terceirizado[0];
        this.totalPages = 0;
    }
}