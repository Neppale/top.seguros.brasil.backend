static class AgeCalculator
{
  public static int Calculate(string date)
  {
    DateTime dateTime = DateTime.Parse(date);
    int age = DateTime.Now.Year - dateTime.Year;
    if (DateTime.Now.Month < dateTime.Month || (DateTime.Now.Month == dateTime.Month && DateTime.Now.Day < dateTime.Day)) age--;
    return age;
  }
}
