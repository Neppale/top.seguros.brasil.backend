namespace tsb.mininal.policy.engine.Utils;

class PasswordHasher
{
    public static string HashPassword(string password)
    {
        string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
        string hash = BCrypt.Net.BCrypt.HashPassword(password, salt);

        return hash;
    }

    public static bool Verify(string hash, string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}



