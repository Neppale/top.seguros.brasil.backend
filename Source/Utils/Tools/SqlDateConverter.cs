static class SqlDateConverter
{
  /* <summary> Esta função converte uma data no formato SQL para o formato brasileiro. </summary> */
  public static string ConvertToShow(string date)
  {
    var dateParts = date.Split(' ');
    var datePart = dateParts[0];
    var timePart = dateParts[1];

    var datePartsSplitted = datePart.Split('-');
    var year = datePartsSplitted[0];
    var month = datePartsSplitted[1];
    var day = datePartsSplitted[2];

    return $"{day}/{month}/{year}";
  }

  /* <summary> Esta função converte uma data no formato brasileiro para o formato SQL. </summary> */
  public static string ConvertToSave(string date)
  {
    var dateParts = date.Split('/');
    var day = dateParts[0];
    var month = dateParts[1];
    var year = dateParts[2];

    return $"{year}-{month}-{day} 00:00:00";
  }
}
