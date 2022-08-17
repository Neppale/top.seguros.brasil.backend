namespace tsb.mininal.policy.engine.Utils;

public static class NullPropertyValidator
{
  public static bool Validate<Classe>(Classe data)
  {
    var properties = typeof(Classe).GetProperties();
    foreach (var property in properties)
    {
      var value = property.GetValue(data, null);
      if (value == null) return false;
    }

    foreach (var property in properties)
    {
      var value = property.GetValue(data, null);
      if (value is string) if (string.IsNullOrEmpty(value as string)) return false;
    }

    return true;
  }
}