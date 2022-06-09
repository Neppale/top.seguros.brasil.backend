interface ModeloClasses<Classe>

{
  public IEnumerable<Classe> Get(string dbConnectionString);
  public IEnumerable<Classe> Get(int id, string dbConnectionString);
  public IResult Insert(Classe classe, string dbConnectionString);
}