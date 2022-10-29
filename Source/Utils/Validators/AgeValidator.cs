static class AgeCalculator
{
    public static int Calculate(string date)
    {
        // dateTime always comes as MM/dd/yyyy. We need to convert it to dd/MM/yyyy to be able to parse it.
        string convertedDate = date.Substring(3, 2) + "/" + date.Substring(0, 2) + "/" + date.Substring(6, 4);
        DateTime birthDate = DateTime.Parse(convertedDate);
        DateTime today = DateTime.Today;
        int age = today.Year - birthDate.Year;
        if (birthDate > today.AddYears(-age)) age--;
        return age;
    }
}
