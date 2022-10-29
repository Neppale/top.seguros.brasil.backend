class PaginatedUsers
{
  public GetUserDto[] users { get; set; }
  public int totalPages { get; set; }

  public PaginatedUsers()
  {
    // Default constructor
    this.users = new GetUserDto[0];
    this.totalPages = 0;
  }

  public PaginatedUsers(GetUserDto[] users, int totalPages)
  {
    this.users = users;
    this.totalPages = totalPages;
  }
}