static class SqlDateConverter
{
    /* <summary> Esta função converte uma data no formato SQL para o formato brasileiro. </summary> */
    public static string Convert(string date)
    {
        Regex sqlFormat = new Regex("[0-9]{4}-[0-9]{2}-[0-9]{2}");
        Regex brazilianFormat = new Regex("[0-9]{2}/[0-9]{2}/[0-9]{4}");

        if (sqlFormat.IsMatch(date)) return date.Substring(8, 2) + "/" + date.Substring(5, 2) + "/" + date.Substring(0, 4);
        if (brazilianFormat.IsMatch(date)) return date.Substring(6, 4) + "-" + date.Substring(3, 2) + "-" + date.Substring(0, 2);

        return date;
    }
    public static DateTime ConvertToDate(string date)
    {
        int day = int.Parse(date.Substring(0, 2));
        int month = int.Parse(date.Substring(3, 2));
        int year = int.Parse(date.Substring(6, 4));

        if (month > 12) return new DateTime(year, day, month);
        else return new DateTime(year, month, day);
    }
}
