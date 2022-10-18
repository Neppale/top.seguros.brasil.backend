public class CepInfo
{
    public string cep { get; set; }
    public string logradouro { get; set; }
    public string complemento { get; set; }
    public string bairro { get; set; }
    public string localidade { get; set; }
    public string uf { get; set; }
    public string ibge { get; set; }
    public string gia { get; set; }
    public string ddd { get; set; }
    public string siafi { get; set; }

    public CepInfo()
    {
        // Default constructor

        cep = "any_cep";
        logradouro = "any_logradouro";
        complemento = "any_complemento";
        bairro = "any_bairro";
        localidade = "any_localidade";
        uf = "any_uf";
        ibge = "any_ibge";
        gia = "any_gia";
        ddd = "any_ddd";
        siafi = "any_siafi";
    }
}