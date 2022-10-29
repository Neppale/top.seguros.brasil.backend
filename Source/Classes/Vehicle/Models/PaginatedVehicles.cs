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

class paginatedResponse
{
  public object[] data { get; set; }
  public int totalPages { get; set; }

  public paginatedResponse()
  {
    // Default constructor
    this.data = new object[0];
    this.totalPages = 0;
  }

  public paginatedResponse(object[] data, int totalPages)
  {
    this.data = data;
    this.totalPages = totalPages;
  }
}