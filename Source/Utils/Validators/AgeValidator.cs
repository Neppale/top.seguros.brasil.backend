static class AgeCalculator
{
    public static int Calculate(string date)
    {
        string[] dateParts = date.Split('-');
        int year = int.Parse(dateParts[0]);
        int month = int.Parse(dateParts[1]);
        int day = int.Parse(dateParts[2]);

        DateTime birthDate = new DateTime(year, month, day);
        DateTime today = DateTime.Today;

        int age = today.Year - birthDate.Year;
        if (birthDate > today.AddYears(-age)) age--;

        return age;
    }
}
