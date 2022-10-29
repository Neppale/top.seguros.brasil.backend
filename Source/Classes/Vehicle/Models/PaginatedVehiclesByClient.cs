class PaginatedVehiclesByClient
{
  public GetVehicleByClientDto[] vehicles { get; set; }
  public int totalPages { get; set; }

  public PaginatedVehiclesByClient()
  {
    // Default constructor
    this.vehicles = new GetVehicleByClientDto[0];
    this.totalPages = 0;
  }

  public PaginatedVehiclesByClient(GetVehicleByClientDto[] vehicles, int totalPages)
  {
    this.vehicles = vehicles;
    this.totalPages = totalPages;
  }
}