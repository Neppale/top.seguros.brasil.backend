namespace tsb.mininal.policy.engine.Utils
{
  public static class DateFormatter
  {
    public static string FormatDate(int year, int month, int day)
    {
      if (month < 1 || month > 12)
      {
        throw new Exception($"Mês {month} está inválido.");
      }

      if (day < 1 || day > 31)
      {
        throw new Exception($"Dia {day} está inválido.");
      }
      return $"{year}-{month}-{day}";
    }
    public static string FormatDate()
    {
      return "1970-01-01";
    }
  }
}
