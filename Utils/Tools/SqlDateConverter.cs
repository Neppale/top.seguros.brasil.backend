static class SqlDateConverter
{
  public static string Convert(string date)
  {
    // Se data estiver formatada como yyyy-MM-dd, converter para dd/MM/yyyy.
    if (date.Contains("-"))
    {
      string[] dateParts = date.Split('-');
      return $"{dateParts[2]}/{dateParts[1]}/{dateParts[0]}";
    }
    // Se data estiver formata como yyyy/MM/dd, converter para dd/MM/yyyy.
    if (date.Contains("/"))
    {
      string[] dateParts = date.Split('/');
      return $"{dateParts[2]}/{dateParts[1]}/{dateParts[0]}";
    }
    return date;
  }
}
