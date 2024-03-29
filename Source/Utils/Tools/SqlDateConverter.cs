static class SqlDateConverter
{
    /* <summary> Esta função converte uma data no formato SQL para o formato brasileiro. </summary> */
    public static string ConvertToShow(string date)
    {
        var dateParts = date.Split(' ');
        var datePart = dateParts[0];
        var timePart = dateParts[1];

        var datePartsSplitted = datePart.Split('/');
        var month = datePartsSplitted[0];
        var day = datePartsSplitted[1];
        var year = datePartsSplitted[2];

        return $"{day}/{month}/{year}";
    }

    /* <summary> Esta função converte uma data no formato brasileiro para o formato SQL. </summary> */
    public static string ConvertToSave(string date)
    {
        var regex = new Regex(@"^\d{4}-\d{2}-\d{2}$");
        if (!regex.IsMatch(date)) return "0000-00-00";

        var dateParts = date.Split('-');
        var year = dateParts[0];
        var month = dateParts[1];
        var day = dateParts[2];

        if (int.Parse(month) > 12) return "0000-00-00";

        return $"{year}-{month}-{day}";
    }
}
