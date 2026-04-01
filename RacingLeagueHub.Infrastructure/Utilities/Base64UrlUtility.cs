namespace RacingLeagueHub.Infrastructure.Utilities;

public static class Base64UrlUtility
{
    // WebEncoders.Base64UrlEncode
    public static string Encode(byte[] input)
    {
        if (input == null || input.Length == 0)
            return string.Empty;

        return Convert.ToBase64String(input)
            .Replace('+', '-')   // URL-safe
            .Replace('/', '_')   // URL-safe
            .TrimEnd('=');       // Remove padding
    }

    // WebEncoders.Base64UrlDecode
    public static byte[] Decode(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Array.Empty<byte>();

        string base64 = input
            .Replace('-', '+')
            .Replace('_', '/');

        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
            case 0:
                break;
            default:
                throw new FormatException("Invalid Base64Url string.");
        }

        return Convert.FromBase64String(base64);
    }
}