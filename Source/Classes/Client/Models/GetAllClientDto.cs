class GetAllClientDto
{
  public int id_cliente { get; set; }
  public string nome_completo { get; set; }
  public string email { get; set; }
  public string cpf { get; set; }
  public string telefone1 { get; set; }


  // Esta classe é uma classe de modelo para o Management Stage.
  public GetAllClientDto(int id_cliente, string nome_completo, string email, string cpf, string telefone1)
  {
    this.id_cliente = id_cliente;
    this.nome_completo = nome_completo;
    this.email = email;
    this.cpf = cpf;
    this.telefone1 = telefone1;
  }

  public GetAllClientDto()
  {
  }
}