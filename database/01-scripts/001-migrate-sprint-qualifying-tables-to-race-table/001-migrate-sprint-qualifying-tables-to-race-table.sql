ALTER TABLE dbo.Race ADD BestTimeTyre nvarchar(2) null;
ALTER TABLE dbo.Race ADD SessionType int not null CONSTRAINT DF_Race_SessionType DEFAULT 4;
ALTER TABLE dbo.Race DROP CONSTRAINT DF_Race_SessionType;

ALTER TABLE dbo.Race ALTER COLUMN Position int not null;
ALTER TABLE dbo.Race ALTER COLUMN TimePenalty int null;
ALTER TABLE dbo.Race ALTER COLUMN PostRaceTimePenalty int null;
ALTER TABLE dbo.Race ALTER COLUMN LapsCompleted int null;
ALTER TABLE dbo.Race ALTER COLUMN GridPosition int null;
ALTER TABLE dbo.Race ALTER COLUMN PointsGained int null;

DECLARE @SprintType int = 2;
DECLARE @QualifyingType int = 3;

BEGIN TRANSACTION
INSERT INTO dbo.Race(TeamId, GrandPrixId, DriverId, Position, FastestLapInMS, ResultStatus, BestTimeTyre, PointsGained, IsReserve, SessionType)
SELECT TeamId, GrandPrixId, DriverId, Position, FastestLapInMS, ResultStatus, BestTimeTyre, PointsGained, IsReserve, @QualifyingType 
FROM dbo.Qualifying;
GO
DROP TABLE dbo.Qualifying;
GO
COMMIT;

BEGIN TRANSACTION
INSERT INTO dbo.Race(TeamId, GrandPrixId, DriverId, Position, RaceTime, TimePenalty, LapsCompleted, GridPosition, PointsGained, IsReserve, UsedTyres, ResultStatus, SessionType)
SELECT TeamId, GrandPrixId, DriverId, Position, RaceTime, TimePenalty, LapsCompleted, GridPosition, PointsGained, IsReserve, UsedTyres, ResultStatus, @SprintType
FROM dbo.Sprint;
GO
DROP TABLE dbo.Sprint;
GO
COMMIT;

EXEC sp_rename 'dbo.Race', 'SessionResult';
EXEC sp_rename 'dbo.PK_RaceId', 'PK_SessionResultId', 'OBJECT';
EXEC sp_rename 'dbo.FK_Race_DriverId', 'FK_SessionResult_DriverId', 'OBJECT';
EXEC sp_rename 'dbo.FK_Race_GrandPrixId', 'FK_SessionResult_GrandPrixId', 'OBJECT';
EXEC sp_rename 'dbo.FK_Race_TeamId', 'FK_SessionResult_TeamId', 'OBJECT';