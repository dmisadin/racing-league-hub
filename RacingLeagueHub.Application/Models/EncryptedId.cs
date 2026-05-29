using RacingLeagueHub.Domain.Utilities;

namespace RacingLeagueHub.Application.Models;

public class EncryptedId
{
    public EncryptedId(long rawId)
    {
        this.RawId = rawId;
        this.EncryptedReadonlyValue = this.RawId.Encrypt();
    }

    public EncryptedId(string encryptedId)
    {
        this.RawId = encryptedId.Decrypt();
        this.EncryptedReadonlyValue = encryptedId;
    }

    public long RawId { get; set; }
    public string EncryptedReadonlyValue { get; set; }
}
