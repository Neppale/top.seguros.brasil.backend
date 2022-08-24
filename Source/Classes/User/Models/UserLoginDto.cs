class UserLoginDto
{
  public string email { get; set; }
  public string senha { get; set; }

  public UserLoginDto(string email, string senha)
  {
    this.email = email;
    this.senha = senha;
  }

  public UserLoginDto()
  {
  }
}