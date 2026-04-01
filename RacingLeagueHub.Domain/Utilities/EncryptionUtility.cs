using System.Buffers.Binary;
using System.Security.Cryptography;

namespace RacingLeagueHub.Domain.Utilities;

public static class EncryptionUtility
{
    private const int NonceSize = 12;
    private const int TagSize = 16;
    private const int CipherSize = 8;
    public const int OutputLength = 48;

    // Lazy ensures the env variable is read once, on first use, not at startup
    private static readonly Lazy<byte[]> _key = new(() =>
    {
        var base64Key = Environment.GetEnvironmentVariable("RACINGLEAGUEHUB_ENCRYPTION_KEY")
            ?? throw new InvalidOperationException(
                "RACINGLEAGUEHUB_ENCRYPTION_KEY environment variable is not set.");

        var key = Convert.FromBase64String(base64Key);

        if (key.Length != 32)
            throw new InvalidOperationException(
                "Encryption key must be exactly 32 bytes (AES-256).");

        return key;
    });

    public static string Encrypt(this long rawId)
    {
        Span<byte> nonce = stackalloc byte[NonceSize];
        Span<byte> plaintext = stackalloc byte[CipherSize];
        Span<byte> ciphertext = stackalloc byte[CipherSize];
        Span<byte> tag = stackalloc byte[TagSize];

        RandomNumberGenerator.Fill(nonce);
        BinaryPrimitives.WriteInt64LittleEndian(plaintext, rawId);

        using var aes = new AesGcm(_key.Value, TagSize);
        aes.Encrypt(nonce, plaintext, ciphertext, tag);

        var output = new byte[NonceSize + CipherSize + TagSize];
        nonce.CopyTo(output.AsSpan(0));
        ciphertext.CopyTo(output.AsSpan(NonceSize));
        tag.CopyTo(output.AsSpan(NonceSize + CipherSize));

        return Base64UrlUtility.Encode(output);
    }

    public static long Decrypt(this string encryptedId)
    {
        if (string.IsNullOrWhiteSpace(encryptedId))
            return 0;

        byte[] data;

        try
        {
            data = Base64UrlUtility.Decode(encryptedId);
        }
        catch (FormatException)
        {
            throw new ArgumentException($"Encrypted ID '{encryptedId}' is not valid Base64Url.");
        }

        if (data.Length != NonceSize + CipherSize + TagSize)
            throw new ArgumentException(
                $"Encrypted ID has invalid length. Expected {NonceSize + CipherSize + TagSize} bytes.");

        var nonce = data.AsSpan(0, NonceSize);
        var ciphertext = data.AsSpan(NonceSize, CipherSize);
        var tag = data.AsSpan(NonceSize + CipherSize, TagSize);

        Span<byte> plaintext = stackalloc byte[CipherSize];

        try
        {
            using var aes = new AesGcm(_key.Value, TagSize);
            aes.Decrypt(nonce, ciphertext, tag, plaintext);
        }
        catch (AuthenticationTagMismatchException)
        {
            throw new ArgumentException(
                $"Encrypted ID '{encryptedId}' is invalid or has been tampered with.");
        }

        return BinaryPrimitives.ReadInt64LittleEndian(plaintext);
    }
}