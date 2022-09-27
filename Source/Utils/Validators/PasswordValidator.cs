static class PasswordValidator
{
    public static bool Validate(string password)
    {
        return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,50}$");
    }
}