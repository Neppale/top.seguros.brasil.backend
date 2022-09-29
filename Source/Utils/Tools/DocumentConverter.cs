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
    public static Stream Decode(string encodedString, string fileType)
    {
        byte[] data = Convert.FromBase64String(encodedString);
        return new MemoryStream(data);
    }
}
