class DocumentConverter
{
  /** <summary>Esta função converte um arquivo em binário para uma string base64.</summary> **/
  public static string Encode(Stream stream)
  {
    // Eu não tenho nem ideia do que estou fazendo. Mas funciona!
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
  public static string Decode(string encodedString)
  {
    byte[] data = Convert.FromBase64String(encodedString);

    // O nome do arquivo é a data atual no momento da requisição.
    string localTime = DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "");
    string fileName = @"C:\Users\Desktop\Downloads\" + $"{localTime}.png";

    // Criando arquivo no diretório de downloads da aplicação.
    using (var image = new FileStream(fileName, FileMode.Create))
    {
      image.Write(data, 0, data.Length);
      image.Flush();
    }

    return fileName;
  }
}
