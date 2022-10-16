class GetAllIncidentsDto
{
    // Adaptado para o Management Stage.
    public IEnumerable<Incident> data { get; set; }
    public int totalPages { get; set; }
    public GetAllIncidentsDto(IEnumerable<Incident> data, int totalPages)
    {
        this.data = data;
        this.totalPages = totalPages;
    }

    public GetAllIncidentsDto()
    {
    }
}

class Incident
{
    public int id_ocorrencia { get; set; }
    public string nome { get; set; }
    public string tipo { get; set; }
    public string data { get; set; }
    public string status { get; set; }

}