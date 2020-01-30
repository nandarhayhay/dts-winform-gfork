SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;
BEGIN TRANSACTION

IF EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD WHERE [UserName] = 'AGUS JUMANTO' AND TypeApp = 'TM')
BEGIN
DELETE FROM UserPriviledgeDPRD WHERE [UserName] = 'AGUS JUMANTO' AND TypeApp = 'TM';
END
INSERT INTO UserPriviledgeDPRD(UserName,TypeApp,TMCode,TerritoryCode,RegionalCode,[Password],CreatedBy,CreatedDate)
VALUES('AGUS JUMANTO','TM','004082','004','047','NFD01_TM_70','Admin',GETDATE());
IF(@@ERROR > 0)
BEGIN ROLLBACK TRANSACTION; RETURN; END
GO
 -----------------------------------------------------------
IF EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD WHERE [UserName] = 'ALEXANDER' AND TypeApp = 'TM')
BEGIN
DELETE FROM UserPriviledgeDPRD WHERE [UserName] = 'ALEXANDER' AND TypeApp = 'TM';
END
INSERT INTO UserPriviledgeDPRD(UserName,TypeApp,TMCode,TerritoryCode,RegionalCode,[Password],CreatedBy,CreatedDate)
VALUES('ALEXANDER','TM','040029','040','045','NFD01_TM_72','Admin',GETDATE());
IF(@@ERROR > 0)
BEGIN ROLLBACK TRANSACTION; RETURN; END
GO
 ----------------------------
IF EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD WHERE [UserName] = 'ALI NUR MAKSIN' AND TypeApp = 'TM')
BEGIN
DELETE FROM UserPriviledgeDPRD WHERE [UserName] = 'ALI NUR MAKSIN' AND TypeApp = 'TM';
END
INSERT INTO UserPriviledgeDPRD(UserName,TypeApp,TMCode,TerritoryCode,RegionalCode,[Password],CreatedBy,CreatedDate)
VALUES('ALI NUR MAKSIN','TM','039085','039','002','NFD01_TM_93','Admin',GETDATE());
IF(@@ERROR > 0)
BEGIN ROLLBACK TRANSACTION; RETURN; END
GO
----------------------------------------------

IF EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD WHERE [UserName] = 'BINA PRIHATTOMON' AND TypeApp = 'TM')
BEGIN
DELETE FROM UserPriviledgeDPRD WHERE [UserName] = 'BINA PRIHATTOMO' AND TypeApp = 'TM';
END
INSERT INTO UserPriviledgeDPRD(UserName,TypeApp,TMCode,TerritoryCode,RegionalCode,[Password],CreatedBy,CreatedDate)
VALUES('BINA PRIHATTOMO','TM','055044','055','024A','NFD01_TM_20','Admin',GETDATE());
IF(@@ERROR > 0)
BEGIN ROLLBACK TRANSACTION; RETURN; END
GO
  ------------------------------------------------------------
IF EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD WHERE [UserName] = 'DADAN MISBAHUDDIN' AND TypeApp = 'TM')
BEGIN
DELETE FROM UserPriviledgeDPRD WHERE [UserName] = 'DADAN MISBAHUDDIN' AND TypeApp = 'TM';
END
INSERT INTO UserPriviledgeDPRD(UserName,TypeApp,TMCode,TerritoryCode,RegionalCode,[Password],CreatedBy,CreatedDate)
VALUES('DADAN MISBAHUDDIN','TM','056051','056','048','NFD01_TM_81','Admin',GETDATE());
IF(@@ERROR > 0)
BEGIN ROLLBACK TRANSACTION; RETURN; END
GO
--------------------------------------------------------------
------------------------------------------------------------------  
IF EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD WHERE [UserName] = 'DJAMALUDIN LAMUSA' AND TypeApp = 'TM')
BEGIN
DELETE FROM UserPriviledgeDPRD WHERE [UserName] = 'DJAMALUDIN LAMUSA' AND TypeApp = 'TM';
END
INSERT INTO UserPriviledgeDPRD(UserName,TypeApp,TMCode,TerritoryCode,RegionalCode,[Password],CreatedBy,CreatedDate)
VALUES('DJAMALUDIN LAMUSA','TM','037042','037','048','NFD01_TM_62','Admin',GETDATE());
IF(@@ERROR > 0)
BEGIN ROLLBACK TRANSACTION; RETURN; END
GO
---------------------------------------------------------------------------
IF EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD WHERE [UserName] = 'GUGUM GUMILAR' AND TypeApp = 'TM')
BEGIN
DELETE FROM UserPriviledgeDPRD WHERE [UserName] = 'GUGUM GUMILAR' AND TypeApp = 'TM';
END
INSERT INTO UserPriviledgeDPRD(UserName,TypeApp,TMCode,TerritoryCode,RegionalCode,[Password],CreatedBy,CreatedDate)
VALUES('GUGUM GUMILAR','TM','031050','031','024A','NFD01_TM_67','Admin',GETDATE());
IF(@@ERROR > 0)
BEGIN ROLLBACK TRANSACTION; RETURN; END
GO
 ------------------------------------------------
IF EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD WHERE [UserName] = 'IRWANSYAH SP' AND TypeApp = 'TM')
BEGIN
DELETE FROM UserPriviledgeDPRD WHERE [UserName] = 'IRWANSYAH SP' AND TypeApp = 'TM';
END
INSERT INTO UserPriviledgeDPRD(UserName,TypeApp,TMCode,TerritoryCode,RegionalCode,[Password],CreatedBy,CreatedDate)
VALUES('IRWANSYAH SP','TM','029060','029','046','NFD01_TM_94','Admin',GETDATE());
IF(@@ERROR > 0)
BEGIN ROLLBACK TRANSACTION; RETURN; END
GO 
  ----------------------------------------------------------------------------------
IF EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD WHERE [UserName] = 'MOCH.KURYADI' AND TypeApp = 'TM')
BEGIN
DELETE FROM UserPriviledgeDPRD WHERE [UserName] = 'MOCH.KURYADI' AND TypeApp = 'TM';
END
INSERT INTO UserPriviledgeDPRD(UserName,TypeApp,TMCode,TerritoryCode,RegionalCode,[Password],CreatedBy,CreatedDate)
VALUES('MOCH.KURYADI','TM','061071','061','047','NFD01_TM_63','Admin',GETDATE());
IF(@@ERROR > 0)
BEGIN ROLLBACK TRANSACTION; RETURN ; END
GO 
  ----------------------------------------------------------------------------------
  
IF EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD WHERE [UserName] = 'KUSMINTO' AND TypeApp = 'TM')
BEGIN
DELETE FROM UserPriviledgeDPRD WHERE [UserName] = 'KUSMINTO' AND TypeApp = 'TM';
END
INSERT INTO UserPriviledgeDPRD(UserName,TypeApp,TMCode,TerritoryCode,RegionalCode,[Password],CreatedBy,CreatedDate)
VALUES('KUSMINTO','TM','040069','040','045','NFD01_TM_61','Admin',GETDATE());
IF(@@ERROR > 0)
BEGIN ROLLBACK TRANSACTION; RETURN; END
GO 
  ----------------------------------------------------------------------------------
IF EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD WHERE [UserName] = 'M. IMAM SOLIHIN' AND TypeApp = 'TM')
BEGIN
DELETE FROM UserPriviledgeDPRD WHERE [UserName] = 'M. IMAM SOLIHIN' AND TypeApp = 'TM';
END
INSERT INTO UserPriviledgeDPRD(UserName,TypeApp,TMCode,TerritoryCode,RegionalCode,[Password],CreatedBy,CreatedDate)
VALUES('M. IMAM SOLIHIN','TM','044083','044','046','NFD01_TM_86','Admin',GETDATE());
IF(@@ERROR > 0)
BEGIN ROLLBACK TRANSACTION; RETURN; END
GO 
  ----------------------------------------------------------------------------------
IF EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD WHERE [UserName] = 'M. HUDA' AND TypeApp = 'TM')
BEGIN
DELETE FROM UserPriviledgeDPRD WHERE [UserName] = 'M. HUDA' AND TypeApp = 'TM';
END
INSERT INTO UserPriviledgeDPRD(UserName,TypeApp,TMCode,TerritoryCode,RegionalCode,[Password],CreatedBy,CreatedDate)
VALUES('M. HUDA','TM','031075','031','024A','NFD01_TM_68','Admin',GETDATE());
IF(@@ERROR > 0)
BEGIN ROLLBACK TRANSACTION; RETURN; END
GO 
  ----------------------------------------------------------------------------------

IF EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD WHERE [UserName] = 'NASTION' AND TypeApp = 'TM')
BEGIN
DELETE FROM UserPriviledgeDPRD WHERE [UserName] = 'NASTION' AND TypeApp = 'TM';
END
INSERT INTO UserPriviledgeDPRD(UserName,TypeApp,TMCode,TerritoryCode,RegionalCode,[Password],CreatedBy,CreatedDate)
VALUES('NASTION','TM','025025','025','046','NFD01_TM_15','Admin',GETDATE());
IF(@@ERROR > 0)
BEGIN ROLLBACK TRANSACTION; RETURN; END
GO 
  ----------------------------------------------------------------------------------

IF EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD WHERE [UserName] = 'NUGROHO ADI' AND TypeApp = 'TM')
BEGIN
DELETE FROM UserPriviledgeDPRD WHERE [UserName] = 'NUGROHO ADI' AND TypeApp = 'TM';
END
INSERT INTO UserPriviledgeDPRD(UserName,TypeApp,TMCode,TerritoryCode,RegionalCode,[Password],CreatedBy,CreatedDate)
VALUES('NUGROHO ADI','TM','054062','054','024A','NFD01_TM_67','Admin',GETDATE());
IF(@@ERROR > 0)
BEGIN ROLLBACK TRANSACTION; RETURN; END
GO 
  ----------------------------------------------------------------------------------
IF EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD WHERE [UserName] = 'REGI DWIJAYA' AND TypeApp = 'TM')
BEGIN
DELETE FROM UserPriviledgeDPRD WHERE [UserName] = 'REGI DWIJAYA' AND TypeApp = 'TM';
END
INSERT INTO UserPriviledgeDPRD(UserName,TypeApp,TMCode,TerritoryCode,RegionalCode,[Password],CreatedBy,CreatedDate)
VALUES('REGI DWIJAYA','TM','023086','023','047','NFD01_TM_104','Admin',GETDATE());
IF(@@ERROR > 0)
BEGIN ROLLBACK TRANSACTION; RETURN; END
GO 
  ----------------------------------------------------------------------------------

IF EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD WHERE [UserName] = 'RUDIONO' AND TypeApp = 'TM')
BEGIN
DELETE FROM UserPriviledgeDPRD WHERE [UserName] = 'RUDIONO' AND TypeApp = 'TM';
END
INSERT INTO UserPriviledgeDPRD(UserName,TypeApp,TMCode,TerritoryCode,RegionalCode,[Password],CreatedBy,CreatedDate)
VALUES('RUDIONO','TM','038004','038','047','NFD01_TM_24','Admin',GETDATE());
IF(@@ERROR > 0)
BEGIN ROLLBACK TRANSACTION; RETURN; END
GO 
  ----------------------------------------------------------------------------------
IF EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD WHERE [UserName] = 'SAEFUL OKTA' AND TypeApp = 'TM')
BEGIN
DELETE FROM UserPriviledgeDPRD WHERE [UserName] = 'SAEFUL OKTA' AND TypeApp = 'TM';
END
INSERT INTO UserPriviledgeDPRD(UserName,TypeApp,TMCode,TerritoryCode,RegionalCode,[Password],CreatedBy,CreatedDate)
VALUES('SAEFUL OKTA','TM','031A063','031A','024A','NFD01_TM_56','Admin',GETDATE());
IF(@@ERROR > 0)
BEGIN ROLLBACK TRANSACTION; RETURN; END
GO 

  ----------------------------------------------------------------------------------
IF EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD WHERE [UserName] = 'SUGITO' AND TypeApp = 'TM')
BEGIN
DELETE FROM UserPriviledgeDPRD WHERE [UserName] = 'SUGITO' AND TypeApp = 'TM';
END
INSERT INTO UserPriviledgeDPRD(UserName,TypeApp,TMCode,TerritoryCode,RegionalCode,[Password],CreatedBy,CreatedDate)
VALUES('SUGITO','TM','016A009','016A','045','NFD01_TM_10','Admin',GETDATE());
IF(@@ERROR > 0)
BEGIN ROLLBACK TRANSACTION; RETURN; END
GO 
  ----------------------------------------------------------------------------------
IF EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD WHERE [UserName] = 'SUGIYONO' AND TypeApp = 'TM')
BEGIN
DELETE FROM UserPriviledgeDPRD WHERE [UserName] = 'SUGIYONO' AND TypeApp = 'TM';
END
INSERT INTO UserPriviledgeDPRD(UserName,TypeApp,TMCode,TerritoryCode,RegionalCode,[Password],CreatedBy,CreatedDate)
VALUES('SUGIYONO','TM','009016','009','045','NFD01_TM_11','Admin',GETDATE());
IF(@@ERROR > 0)
BEGIN ROLLBACK TRANSACTION; RETURN; END
GO 

  ----------------------------------------------------------------------------------
IF EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD WHERE [UserName] = 'WAHYUDI SUBAGYO' AND TypeApp = 'TM')
BEGIN
DELETE FROM UserPriviledgeDPRD WHERE [UserName] = 'WAHYUDI SUBAGYO' AND TypeApp = 'TM';
END
INSERT INTO UserPriviledgeDPRD(UserName,TypeApp,TMCode,TerritoryCode,RegionalCode,[Password],CreatedBy,CreatedDate)
VALUES('WAHYUDI SUBAGYO','TM','026026','026','046','NFD01_TM_16','Admin',GETDATE());
IF(@@ERROR > 0)
BEGIN ROLLBACK TRANSACTION; RETURN; END
GO 
  ----------------------------------------------------------------------------------
IF EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD WHERE [UserName] = 'WIDY KURNIAWAN' AND TypeApp = 'TM')
BEGIN
DELETE FROM UserPriviledgeDPRD WHERE [UserName] = 'WIDY KURNIAWAN' AND TypeApp = 'TM';
END
INSERT INTO UserPriviledgeDPRD(UserName,TypeApp,TMCode,TerritoryCode,RegionalCode,[Password],CreatedBy,CreatedDate)
VALUES('WIDY KURNIAWAN','TM','032061','032','002','NFD01_TM_37','Admin',GETDATE());
IF(@@ERROR > 0)
BEGIN ROLLBACK TRANSACTION; RETURN; END
GO 
  ----------------------------------------------------------------------------------
UPDATE UserPriviledgeDPRD SET [UserName] = LTRIM(RTRIM(UserName)),[Password] = LTRIM(RTRIM([Password])),
TerritoryCode = LTRIM(RTRIM(TerritoryCode)),TypeApp = LTRIM(RTRIM(TypeApp)),TMCode = LTRIM(RTRIM(TMCode)),
RegionalCode = LTRIM(RTRIM(RegionalCode));
IF(@@ERROR > 0)
BEGIN ROLLBACK TRANSACTION; RETURN; END
GO

IF EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD WHERE [UserName] = 'TAVIP KUPIYOTOMO' AND TypeApp = 'TM')
BEGIN
DELETE FROM UserPriviledgeDPRD WHERE [UserName] = 'TAVIP KUPIYOTOMO' AND TypeApp = 'TM';
END
INSERT INTO UserPriviledgeDPRD(UserName,TypeApp,TMCode,TerritoryCode,RegionalCode,[Password],CreatedBy,CreatedDate)
VALUES('TAVIP KUPIYOTOMO','TM','032061','032','002','NFD01_TM_26','Admin',GETDATE());
IF(@@ERROR > 0)
BEGIN ROLLBACK TRANSACTION; RETURN; END
GO 


IF (@@TRANCOUNT > 0)
BEGIN COMMIT TRANSACTION ; END 
SELECT * FROM UserPriviledgeDPRD WHERE TypeApp = 'TM'
DELETE FROM UserpriviledgeDPRD WHERE LEN([Password]) > 12;
DELETE FROM UserPriviledgeDPRD WHERE [Password] is NULL OR [Password] = '';

SELECT [ROWS] FROM sysindexes  WHERE id = OBJECT_ID('UserPriviledgeDPRD') AND INDID < 2) 

exec sp_executesql @stmt = N'SET NOCOUNT ON;IF EXISTS(SELECT MANAGER FROM
(SELECT ST.TERRITORY_ID AS TerritoryCode,ST.INACTIVE,TM.MANAGER FROM SHIP_TO ST INNER JOIN TERRITORY_MANAGER TM
 ON TM.TM_ID = ST.TM_ID)TM WHERE NOT EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD UP WHERE UP.[UserName] = TM.MANAGER
 AND UP.TerritoryCode = TM.TerritoryCode AND UP.TypeApp = ''TM'') AND TM.INACTIVE = 0)
 BEGIN
SELECT * FROM (SELECT TER.REGIONAL_ID AS RegionalCode,ST.SHIP_TO_ID AS TMCode,ST.TERRITORY_ID AS TerritoryCode,ST.INACTIVE,TM.MANAGER AS TerritoryManager,TM.HP,
ST.CREATE_BY AS CreatedBy,ST.CREATE_DATE AS CreatedDate  FROM SHIP_TO ST INNER JOIN TERRITORY_MANAGER TM
 ON TM.TM_ID = ST.TM_ID INNER JOIN TERRITORY TER ON TER.TERRITORY_ID = ST.TERRITORY_ID)TM 
 WHERE NOT EXISTS(SELECT [UserName] FROM UserPriviledgeDPRD UP WHERE UP.[UserName] = TM.TerritoryManager
 AND UP.TerritoryCode = TM.TerritoryCode AND UP.TypeApp = ''TM'') AND TM.INACTIVE = 0 OPTION(KEEP PLAN);
 END '

exec sp_executesql @stmt = N'SET NOCOUNT ON;IF EXISTS(SELECT UserName,Count(UserName) AS JUMLAH FROM UserPriviledgeDPRD 
GROUP BY UserName HAVING Count(UserName) > 1)
 BEGIN 
 SELECT IDApp,[UserName],TypeApp FROM UserPriviledgeDPRD WHERE [UserName] IN(
SELECT [UserName] FROM(SELECT [UserName],Count([UserName]) AS JUMLAH FROM UserPriviledgeDPRD 
GROUP BY [UserName] HAVING Count(UserName) > 1)UP) OPTION(KEEP PLAN);
END '

SET NOCOUNT ON;IF EXISTS(SELECT UserName,Count(UserName) AS JUMLAH,TypeApp FROM UserPriviledgeDPRD 
                                GROUP BY UserName,TypeApp HAVING Count(UserName) > 1 AND Count(TypeApp) > 1)
                                 BEGIN 
                                 SELECT IDApp,[UserName],TypeApp FROM UserPriviledgeDPRD WHERE [UserName] IN(
                                SELECT [UserName] FROM(SELECT [UserName],Count([UserName]) AS JUMLAH FROM UserPriviledgeDPRD 
                                GROUP BY [UserName],TypeApp HAVING Count(UserName) > 1 AND Count(TypeApp) > 1)UP) OPTION(KEEP PLAN);
                                END