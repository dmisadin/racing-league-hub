ALTER TABLE dbo.Season DROP CONSTRAINT FK_Season_GameId;
ALTER TABLE dbo.Season DROP CONSTRAINT FK_Season_PlatformId;
ALTER TABLE dbo.Driver DROP CONSTRAINT FK_Driver_PlatformId;
ALTER TABLE dbo.League DROP CONSTRAINT FK_League_RegionId;

DROP TABLE dbo.Region;
DROP TABLE dbo.Game;
DROP TABLE dbo.[Platform];

EXEC sp_rename 'Season.GameId', 'Game', 'COLUMN';
EXEC sp_rename 'Season.PlatformId', 'Platform', 'COLUMN';
EXEC sp_rename 'Driver.PlatformId', 'Platform', 'COLUMN';
EXEC sp_rename 'League.RegionId', 'Region', 'COLUMN';

DECLARE @PC int = 1;
DECLARE @Steam int = 2;
DECLARE @F12019 int = 9;
DECLARE @F12020 int = 10;
DECLARE @F12021 int = 11;
DECLARE @F122 int = 12;

UPDATE dbo.Season SET [Platform] = @Steam WHERE [Platform] = @PC;
UPDATE dbo.Season SET Game = @F12019 WHERE Game = 1;
UPDATE dbo.Season SET Game = @F12020 WHERE Game = 2;
UPDATE dbo.Season SET Game = @F12021 WHERE Game = 3;
UPDATE dbo.Season SET Game = @F122 WHERE Game = 4;
UPDATE dbo.Driver SET [Platform] = @Steam WHERE [Platform] = @PC;