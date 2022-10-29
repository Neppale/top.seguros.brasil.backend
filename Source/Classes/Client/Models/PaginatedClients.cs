class PaginatedClients
{
    public GetAllClientDto[] clients { get; set; }
    public int totalPages { get; set; }

    public PaginatedClients(GetAllClientDto[] clients, int totalPages)
    {
        this.clients = clients;
        this.totalPages = totalPages;
    }

    public PaginatedClients()
    {
    }
}