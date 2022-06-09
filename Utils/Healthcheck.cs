namespace tsb.mininal.policy.engine.Utils
{
  public abstract class HealthCheck
  {
    public static IResult Check(string endpoint)
    {

      return Results.Accepted();
    }
    public static string FormatDate()
    {
      return "1970-01-01";
    }
  }
}
