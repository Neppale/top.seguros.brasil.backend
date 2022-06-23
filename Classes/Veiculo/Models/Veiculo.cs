public class Veiculo
{
  public int id_veiculo { get; set; }
  public string marca { get; set; }
  public string modelo { get; set; }
  public int ano { get; set; }
  public string uso { get; set; }
  public string placa { get; set; }
  public string renavam { get; set; }
  public bool sinistrado { get; set; }
  public int id_cliente { get; set; }
  public bool status { get; set; }

  public Veiculo(string brand, string model, int year, string usage, string plate, string renavam, bool sinistrado, int idcliente, bool status)
  {
    this.marca = brand;
    this.modelo = model;
    this.ano = year;
    this.uso = usage;
    this.placa = plate;
    this.renavam = renavam;
    this.sinistrado = sinistrado;
    this.id_cliente = idcliente;
    this.status = status;
  }
  public Veiculo()
  {
  }


}