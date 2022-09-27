public class Veiculo
{
    public int id_veiculo { get; set; }
    public string marca { get; set; }
    public string modelo { get; set; }
    public string ano { get; set; }
    public string uso { get; set; }
    public string placa { get; set; }
    public string renavam { get; set; }
    public bool sinistrado { get; set; }
    public int id_cliente { get; set; }
    public bool status { get; set; }

    public Veiculo(string marca, string modelo, string ano, string uso, string placa, string renavam, bool sinistrado, int id_cliente, bool status)
    {
        this.marca = marca;
        this.modelo = modelo;
        this.ano = ano;
        this.uso = uso;
        this.placa = placa;
        this.renavam = renavam;
        this.sinistrado = sinistrado;
        this.id_cliente = id_cliente;
        this.status = status;
    }
    public Veiculo()
    {
    }


}