namespace RacingLeagueHub.Application.Models.Resource;

public record FileUploadRequest(
    string FileName,
    string ContentType,
    long SizeInBytes,
    Stream Content
);