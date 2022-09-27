static class IncidentDateValidator
{
    public static bool Validate(string date)
    {
        DateTime convertedDate = DateTime.Parse(date);
        if (convertedDate > DateTime.Now) return false;

        return true;
    }
}