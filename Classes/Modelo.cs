interface ModeloClasses<Classe>

{
  public IEnumerable<Classe> Get(string dbConnectionString);
  public IEnumerable<Classe> Get(int id, string dbConnectionString);
  /** <summary> Insere no banco de dados. </summary>**/
  public string Insert(Classe classe, string dbConnectionString);
}