class PaginatedIncidents
{
    public GetAllIncidentsDto[] incidents { get; set; }
    public int totalPages { get; set; }

    public PaginatedIncidents()
    {
        // Default constructor
        this.incidents = new GetAllIncidentsDto[0];
        this.totalPages = 0;
    }

    public PaginatedIncidents(GetAllIncidentsDto[] incidents, int totalPages)
    {
        this.incidents = incidents;
        this.totalPages = totalPages;
    }
}