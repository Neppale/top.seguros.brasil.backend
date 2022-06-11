using System.Text.RegularExpressions;
namespace tsb.mininal.policy.engine.Utils
{
  public abstract class RenavamValidator
  {
    public static bool Validate(string RENAVAM)
    {
      if (string.IsNullOrEmpty(RENAVAM.Trim())) return false;

      int[] d = new int[11];
      string sequencia = "3298765432";
      string SoNumero = Regex.Replace(RENAVAM, "[^0-9]", string.Empty);

      if (string.IsNullOrEmpty(SoNumero)) return false;

      //verificando se todos os numeros são iguais **************************
      if (new string(SoNumero[0], SoNumero.Length) == SoNumero) return false;
      SoNumero = Convert.ToInt64(SoNumero).ToString("00000000000");

      int v = 0;

      for (int i = 0; i < 11; i++)
        d[i] = Convert.ToInt32(SoNumero.Substring(i, 1));

      for (int i = 0; i < 10; i++)
        v += d[i] * Convert.ToInt32(sequencia.Substring(i, 1));

      v = (v * 10) % 11; v = (v != 10) ? v : 0;
      return (v == d[10]);
    }
  }
}