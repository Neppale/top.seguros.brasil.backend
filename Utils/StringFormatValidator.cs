using System.Text.RegularExpressions;
abstract public class StringFormatValidator
{
  static public bool ValidateCPF(string cpf)
  {
    Regex regex = new Regex(@"(^\d{3}\x2E\d{3}\x2E\d{3}\x2D\d{2}$)");

    if (!regex.IsMatch(cpf)) return false;
    return true;
  }
  static public bool ValidateCNPJ(string cnpj)
  {
    Regex regex = new Regex(@"(^\d{2}.\d{3}.\d{3}/\d{4}-\d{2}$)");

    if (!regex.IsMatch(cnpj)) return false;
    return true;

  }

  static public bool ValidateTelefone(string telefone)
  {
    Regex regex = new Regex(@"^\(\d\d\)\s\d\d\d\d\d-\d\d\d\d$");

    if (!regex.IsMatch(telefone)) return false;
    return true;

  }

}