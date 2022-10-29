class PaginatedVehicles
{
  public GetAllVehicleDto[] vehicles { get; set; }
  public int totalPages { get; set; }

  public PaginatedVehicles()
  {
    // Default constructor
    this.vehicles = new GetAllVehicleDto[0];
    this.totalPages = 0;
  }

  public PaginatedVehicles(GetAllVehicleDto[] vehicles, int totalPages)
  {
    this.vehicles = vehicles;
    this.totalPages = totalPages;
  }
}