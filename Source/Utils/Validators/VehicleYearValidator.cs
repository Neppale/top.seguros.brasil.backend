static class VehicleYearValidator
{
    private static string[] approvedFuels = new string[] { "Gasolina", "Alcool", "Diesel", "Flex", "Tetrafuel", "Alcool/Gasolina" };
    /**<summary> Valida se o ano do veículo está no formato adequado. </summary>*/
    public static bool Validate(string year)
    {
        Regex regex = new Regex(@"[0-9]+ \w");
        bool isValid = regex.IsMatch(year);
        isValid = approvedFuels.Contains(year.Split(' ')[1]);

        if (!isValid) return false;
        return true;
    }
}