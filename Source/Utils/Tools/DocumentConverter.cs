class DocumentConverter
{
  /** <summary>Esta função converte um arquivo em binário para uma string base64.</summary> **/
  public static string Encode(Stream stream)
  {
    byte[] bytes;
    using (var memoryStream = new MemoryStream())
    {
      stream.CopyTo(memoryStream);
      bytes = memoryStream.ToArray();
    }
    string base64 = Convert.ToBase64String(bytes);

    return base64;
  }
  /** <summary>Esta função converte uma string base64 para um arquivo binário, e retorna o local do arquivo.</summary> **/
  public static string Decode(string encodedString, string fileType)
  {
    byte[] data = Convert.FromBase64String(encodedString);

    if (fileType == "image/png") fileType = "png";
    if (fileType == "image/jpg") fileType = "jpg";
    if (fileType == "image/jpeg") fileType = "jpeg";
    if (fileType == "application/pdf") fileType = "pdf";

    string temporaryDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Temp");

    string filePath = Path.Combine(temporaryDirectory, $"{DateTime.Now.ToString("yyyy-MM-dd")}-{Guid.NewGuid()}.{fileType}");

    using (var image = new FileStream(filePath, FileMode.Create))
    {
      image.Write(data, 0, data.Length);
      image.Flush();
    }

    return filePath;
  }
}
