using System.Text.RegularExpressions;
abstract public class StringFormatValidator
{
  /** <summary> Esta função valida a formatação do CPF informado. </summary>**/
  static public bool ValidateCPF(string cpf)
  {
    Regex regex = new Regex(@"(^\d{3}\x2E\d{3}\x2E\d{3}\x2D\d{2}$)");

    if (!regex.IsMatch(cpf)) return false;
    return true;
  }
  /** <summary> Esta função valida a formatação do CNPJ informado. </summary>**/
  static public bool ValidateCNPJ(string cnpj)
  {
    Regex regex = new Regex(@"(^\d{2}.\d{3}.\d{3}/\d{4}-\d{2}$)");

    if (!regex.IsMatch(cnpj)) return false;
    return true;

  }
  /** <summary> Esta função valida a formatação do telefone informado. </summary>**/
  static public bool ValidateTelefone(string? telefone)
  {
    Regex regex = new Regex(@"^\(\d\d\)\s\d\d\d\d\d-\d\d\d\d$");

    if (telefone == null || telefone == "") return true; // Telefone não informado deve ser considerado válido.

    if (!regex.IsMatch(telefone)) return false;
    return true;

  }

}