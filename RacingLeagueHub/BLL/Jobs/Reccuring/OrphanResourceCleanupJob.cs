using RacingLeagueHub.BLL.Services.Interfaces;
using RacingLeagueHub.Data.Repositories;

namespace RacingLeagueHub.BLL.Jobs.Reccuring;

public class OrphanResourceCleanupJob(
    IServiceScopeFactory scopeFactory,
    ILogger<OrphanResourceCleanupJob> logger) : BackgroundService
{
    // Unconfirmed resources older than this are considered orphans
    private static readonly TimeSpan OrphanAge = TimeSpan.FromHours(24);

    // How often the job runs
    private static readonly TimeSpan Interval = TimeSpan.FromHours(24);

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        logger.LogInformation("Orphan resource cleanup job started.");

        while (!ct.IsCancellationRequested)
        {
            await CleanupAsync(ct);
            await Task.Delay(Interval, ct);
        }
    }

    private async Task CleanupAsync(CancellationToken ct)
    {
        try
        {
            // BackgroundService is a singleton — resolve scoped services via scope factory
            await using var scope = scopeFactory.CreateAsyncScope();
            var resourceRepository = scope.ServiceProvider.GetRequiredService<IResourceRepository>();
            var storageService = scope.ServiceProvider.GetRequiredService<IStorageService>();

            var cutoff = DateTimeOffset.UtcNow - OrphanAge;
            var orphans = await resourceRepository.GetPendingOlderThanAsync(cutoff, ct);

            if (orphans.Count == 0)
            {
                logger.LogInformation("Orphan cleanup: no orphans found.");
                return;
            }

            logger.LogInformation("Orphan cleanup: found {Count} orphan(s) to delete.", orphans.Count);

            foreach (var resource in orphans)
            {
                try
                {
                    var s3Key = $"uploads/{resource.StorageId}.{resource.Extension}";
                    await storageService.DeleteAsync(s3Key, ct);
                    await resourceRepository.DeleteAsync(resource, ct);
                    logger.LogInformation("Deleted orphan resource {Uid}.", resource.StorageId);
                }
                catch (Exception ex)
                {
                    // Log and continue — don't let one failure stop the rest
                    logger.LogError(ex, "Failed to delete orphan resource {Uid}.", resource.StorageId);
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Orphan resource cleanup job failed.");
        }
    }
}

