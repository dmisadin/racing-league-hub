EXEC sp_rename 'SeasonRacePoints', 'SeasonPoints';
ALTER TABLE dbo.SeasonPoints ADD PointsType int not null CONSTRAINT DF_SeasonPoints_PointsType DEFAULT 1; --Default: RaceType
ALTER TABLE dbo.SeasonPoints DROP CONSTRAINT DF_SeasonPoints_PointsType;

DECLARE @RaceType int = 1;
DECLARE @SprintType int = 2;
DECLARE @QualifyingType int = 3;
DECLARE @FastestLapType int = 4;

BEGIN TRANSACTION;
INSERT INTO dbo.SeasonPoints(SeasonId, Points, Position, PointsType)
SELECT Id, Points, Position, @QualifyingType
FROM SeasonQualPoints
WHERE Points > 0;

DROP TABLE dbo.SeasonQualPoints;
COMMIT;

BEGIN TRANSACTION;
INSERT INTO dbo.SeasonPoints(SeasonId, Points, Position, PointsType)
SELECT Id, Points, Position, @SprintType
FROM SeasonSprintPoints
WHERE Points > 0;

DROP TABLE dbo.SeasonSprintPoints;
COMMIT;

BEGIN TRANSACTION;
INSERT INTO dbo.SeasonPoints(SeasonId, Points, Position, PointsType)
SELECT Id, Points, Position, @FastestLapType
FROM SeasonFastestLapPoints
WHERE Points > 0;

DROP TABLE dbo.SeasonFastestLapPoints;
COMMIT;

EXEC sp_rename N'dbo.PK_SeasonRacePointsId', N'PK_SeasonPointsId', N'OBJECT';
EXEC sp_rename N'dbo.FK_SeasonRacePoints_SeasonId', N'FK_SeasonPoints_SeasonId', N'OBJECT';