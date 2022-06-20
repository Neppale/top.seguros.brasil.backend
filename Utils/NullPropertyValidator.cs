namespace tsb.mininal.policy.engine.Utils;

public static class NullPropertyValidator
{
  public static bool Validate<Classe>(Classe data)
  {
    bool notHaveNull = data.GetType().GetProperties()
                              .All(p => p.GetValue(data) != null);

    bool notHaveEmptyString = data.GetType().GetProperties()
    .All(p => p.GetValue(data) != "");

    if (!notHaveNull || !notHaveEmptyString) return false; //  Inválido
    return true; // Válido
  }
}