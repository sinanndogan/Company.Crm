using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace Company.Framework.Authentication;

public static class SecurityHelper
{
    public static byte[] GenerateSalt()
    {
        byte[] salt = new byte[128 / 8];
        using (var generator = RandomNumberGenerator.Create())
        {
            generator.GetBytes(salt);
        }

        return salt;
    }

    public static string HashCreate(string password)
    {
        var salt = GenerateSalt();

        var bytes = KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 16);

        return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(bytes)}";
    }

    public static bool HashValidate(string hash, string value)
    {
        try
        {
            var parts = hash.Split(':');
            var salt = Convert.FromBase64String(parts[0]);
            var bytes = KeyDerivation.Pbkdf2(value, salt, KeyDerivationPrf.HMACSHA1, 10000, 16);

            return parts[1].Equals(Convert.ToBase64String(bytes));
        }
        catch
        {
            return false;
        }
    }

    public static string CreateMd5(string input)
    {
        using var md5 = MD5.Create();
        var inputBytes = Encoding.ASCII.GetBytes(input);
        var hashBytes = md5.ComputeHash(inputBytes);

        return Convert.ToHexString(hashBytes);
    }

    public static string CreateToken()
    {
        return Convert.ToBase64String(GenerateSalt());
    }
}