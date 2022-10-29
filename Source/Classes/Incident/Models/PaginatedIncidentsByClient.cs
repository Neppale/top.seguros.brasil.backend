class PaginatedIncidentsByClient
{
    public GetIncidentByIdDto[] incidents { get; set; }
    public int totalPages { get; set; }

    public PaginatedIncidentsByClient()
    {
        // Default constructor
        this.incidents = new GetIncidentByIdDto[0];
        this.totalPages = 0;
    }

    public PaginatedIncidentsByClient(GetIncidentByIdDto[] incidents, int totalPages)
    {
        this.incidents = incidents;
        this.totalPages = totalPages;
    }
}