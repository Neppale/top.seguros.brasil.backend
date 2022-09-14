static class SqlDateConverter
{
  public static string Convert(string date)
  {
    string[] dateParts = date.Split('/');
    string convertedDate = $"{dateParts[2]}/{dateParts[1]}/{dateParts[0]}";
    return convertedDate;
  }
}
