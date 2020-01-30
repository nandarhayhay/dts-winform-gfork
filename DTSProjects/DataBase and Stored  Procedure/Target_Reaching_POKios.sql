IF EXISTS(SELECT NAME FROM DBO.SYSOBJECTS WHERE NAME = 'Usp_Target_Reaching_Kios' AND TYPE = 'P')
DROP PROCEDURE Usp_Target_Reaching_Kios
GO
CREATE PROCEDURE Usp_Target_Reaching_Kios
@SpCode VARCHAR(16)
AS
SET NOCOUNT ON;
DECLARE @V_StartYear INT,@V_EndYear INT,@V_SalesOrderStartYear VARCHAR(50),
@V_SalesOrderEndYear VARCHAR(50),@V_StartDate DATETIME,@V_EndDate DATETIME,
@V_POHeaderStartYear VARCHAR(50),@V_POHeaderEndYear VARCHAR(50),
@V_SqlQuery NVARCHAR(4000)
IF NOT EXISTS(SELECT PROGRAM_ID FROM MRKT_MARKETING_PROGRAM WHERE PROGRAM_ID = @SpCode)
   BEGIN
 	RAISERROR('Can not find such program id',16,1)
        RETURN;
   END
SET @V_StartDate = (SELECT START_DATE FROM MRKT_MARKETING_PROGRAM WHERE PROGRAM_ID = @SpCode);
SET @V_EndDate = (SELECT END_DATE FROM MRKT_MARKETING_PROGRAM WHERE PROGRAM_ID = @SpCode);
SET @V_StartYear = (SELECT YEAR(@V_StartDate));
SET @V_EndYear = (SELECT YEAR(@V_EndDate));
SET @V_SalesOrderStartYear = ('SalesOrdersDetail_' + CAST(@V_StartYear AS VARCHAR(5)));
SET @V_POHeaderStartYear = ('POHeader_' + CAST(@V_StartYear AS VARCHAR(5)));
SET @V_POHeaderEndYear = ('POHeader_' + CAST(@V_EndYear AS VARCHAR(5)));
IF (@V_EndYear > @V_StartYear)
    BEGIN
	SET @V_SalesOrderEndYear = ('SalesOrdersDetail_' + CAST(@V_EndYear AS VARCHAR(5)));
    END
ELSE 
   BEGIN
	SET @V_SalesOrderEndYear = '';
   END
IF NOT EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'base table'
   AND TABLE_NAME = + @V_SalesOrderStartYear) 
   BEGIN
      RAISERROR('PO  does not exist ',16,1);
      RETURN;
   END
---------------------------------------------------------------------------------------
execute sp_executesql N'SELECT K.IDKios,K.Kios_Name,''Address'' = 
 CASE  
 WHEN K.Address IS NOT NULL AND K.Address NOT LIKE '''' THEN K.Address
 WHEN LTRIM(ISNULL(K.District2,'''') + '' '' + ISNULL(K.District1,'''') + '' '' + ISNULL(K.City,'''') +
         '' '' + ISNULL(K.State,'''')) != '''' THEN 
      LTRIM(ISNULL(K.District2,'''') + '' '' + ISNULL(K.District1,'''') + '' '' + ISNULL(K.City,'''') +
         '' '' + ISNULL(K.State,''''))
 ELSE ''Unspecified Address'' 
 END,
 K.Kios_Owner,Brch.BrandCode AS BRAND_ID,BR.BRAND_NAME,Brch.TotalActual,Brch.Target,Brch.Dispro AS DisproSP,Brch.DisproPercentage
  AS ActualDispro,Brch.BonusQty AS TotalBonus
FROM Kios K INNER JOIN BrandReaching Brch ON K.IDKios = Brch.KiosCode
INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = Brch.BrandCode
WHERE Brch.SpCode = @V_SpCode OPTION(KEEPFIXED PLAN)',N'@V_SpCode VARCHAR(16)',@V_SpCode = @SpCode;
------------------------------------------------------------------------------------
 IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE ='base table'
        AND TABLE_NAME =  @V_SalesOrderEndYear )
  BEGIN
	SET @V_SqlQuery = 'SELECT Brch.KiosCode AS IDKios,K.Kios_Name,Brch.BrandCode AS BRAND_ID,BR.BRAND_NAME,Drch.BrandPackCode AS BRANDPACK_ID,
	BP.BRANDPACK_NAME + '',TotalQty : '' + CAST(Drch.TotalQty AS VARCHAR(10)) + 
	'' BonusQty : '' + CAST(Drch.DiscQty AS VARCHAR(10)) AS [Description],SO.TPOFromDistributor AS TotalPOFromDistributor,
	SO.DistributorCode AS DISTRIBUTOR_ID,DR.DISTRIBUTOR_NAME
	FROM Kios K INNER JOIN BrandReaching Brch ON K.IDKios = Brch.KiosCode 
	INNER JOIN BRND_BRAND BR ON Brch.BrandCode = BR.BRAND_ID 
	INNER JOIN DetailReaching Drch ON Drch.FkCode = Brch.CodeApp
	INNER JOIN BRND_BRANDPACK BP
	ON Drch.BrandPackCode = BP.BRANDPACK_ID
	INNER JOIN 
	(
	  SELECT KiosCode,DistributorCode,BrandPackCode,ISNULL(SUM(TotalPODistributor),0)AS TPOFromDistributor
          FROM
	      (
	  	SELECT SOH.KiosCode,SOH.DistributorCode,SOD.BrandPackCode,ISNull(SUM(SOD.Quantity),0)AS TotalPODistributor
	  	FROM  ' + @V_POHeaderStartYear + ' SOH INNER JOIN ' +  @V_SalesOrderStartYear + ' SOD
	  	ON SOH.CodeApp = SOD.FkCode WHERE SOH.SalesDate >= @StartDate
	  	AND SOH.DistributorCode IN(SELECT DISTINCT DISTRIBUTOR_ID FROM MRKT_BRANDPACK_DISTRIBUTOR
	     	WHERE PROG_BRANDPACK_ID IN(SELECT PROG_BRANDPACK_ID FROM MRKT_BRANDPACK
	     	WHERE PROGRAM_ID = @V_SpCode))
	 	GROUP BY SOH.KiosCode,SOH.DistributorCode,SOD.BrandPackCode

		UNION ALL

   	        SELECT SOH.KiosCode,SOH.DistributorCode,SOD.BrandPackCode,ISNull(SUM(SOD.Quantity),0)AS TotalPODistributor
	  	FROM ' + @V_POHeaderEndYear + ' SOH INNER JOIN ' + @V_SalesOrderEndYear + ' SOD 
	  	ON SOH.CodeApp = SOD.FkCode WHERE SOH.SalesDate <= @EndDate AND SOH.DistributorCode
	  	IN(SELECT DISTINCT DISTRIBUTOR_ID FROM MRKT_BRANDPACK_DISTRIBUTOR
	     	WHERE PROG_BRANDPACK_ID IN(SELECT PROG_BRANDPACK_ID FROM MRKT_BRANDPACK
	     	WHERE PROGRAM_ID = @V_SpCode))
	 	GROUP BY SOH.KiosCode,SOH.DistributorCode,SOD.BrandPackCode
	       )SO
		GROUP BY KiosCode,DistributorCode,BrandPackCode
	)SO ON Drch.BrandPackCode = SO.BrandPackCode AND Brch.KiosCode = SO.KiosCode
	 INNER JOIN DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = SO.DistributorCode 
	WHERE Brch.SpCode = @V_SpCode OPTION(KEEPFIXED PLAN);'
   END
ELSE
   BEGIN
	SET @V_SqlQuery = 'SELECT Brch.KiosCode AS IDKios,K.Kios_Name,Brch.BrandCode AS BRAND_ID,BR.BRAND_NAME,Drch.BrandPackCode AS BRANDPACK_ID,
	BP.BRANDPACK_NAME,BP.BRANDPACK_NAME  + '',TotalQty : '' + CAST(Drch.TotalQty AS VARCHAR(10)) + 
	'' BonusQty : '' + CAST(Drch.DiscQty AS VARCHAR(10)) AS [Description],
	SO.TPOFromDistributor AS TotalPOFromDistributor,SO.DistributorCode AS DISTRIBUTOR_ID,
	DR.DISTRIBUTOR_NAME
	FROM Kios K INNER JOIN BrandReaching Brch ON K.IDKios = Brch.KiosCode 
	INNER JOIN BRND_BRAND BR ON Brch.BrandCode = BR.BRAND_ID 
	INNER JOIN DetailReaching Drch ON Drch.FkCode = Brch.CodeApp
	INNER JOIN BRND_BRANDPACK BP
	ON Drch.BrandPackCode = BP.BRANDPACK_ID
	INNER JOIN 
	(
	  SELECT SOH.KiosCode,SOH.DistributorCode,SOD.BrandPackCode,ISNull(SUM(SOD.Quantity),0)AS TPOFromDistributor
	  FROM ' + @V_POHeaderStartYear + ' SOH INNER JOIN ' + @V_SalesOrderStartYear + ' SOD 
	  ON SOH.CodeApp = SOD.FkCode WHERE SOH.SalesDate >= @StartDate
	  AND SOH.SalesDate <= @EndDate AND SOH.DistributorCode
	  IN(SELECT DISTINCT DISTRIBUTOR_ID FROM MRKT_BRANDPACK_DISTRIBUTOR
	     WHERE PROG_BRANDPACK_ID IN(SELECT PROG_BRANDPACK_ID FROM MRKT_BRANDPACK
	     WHERE PROGRAM_ID = @V_SpCode))
	 GROUP BY SOH.KiosCode,SOH.DistributorCode,SOD.BrandPackCode
        )SO ON Drch.BrandPackCode = SO.BrandPackCode AND Brch.KiosCode = SO.KiosCode 
	 INNER JOIN DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = SO.DistributorCode 
	WHERE Brch.SpCode = @V_SpCode OPTION(KEEPFIXED PLAN);'
   END
exec sp_executesql @V_SqlQuery, 
     N'@V_SpCode VARCHAR(16),@StartDate SMALLDATETIME,@EndDate SMALLDATETIME,
      @StartYear INT,@EndYear INT',
      @V_SpCode = @SpCode,
      @StartDate=@V_StartDate,@EndDate=@V_EndDate,@StartYear=@V_StartYear,@EndYear = @V_EndYear;
----------------------------------------------------------------------------------------
execute sp_executesql N'SELECT K.IDKios,K.Kios_Name FROM Kios K 
INNER JOIN
          (
            SELECT DISTINCT KiosCode AS IDKios FROM BrandReaching WHERE SpCode = @V_SpCode
          )Brch
           ON K.IDKios = Brch.IDKios OPTION(KEEPFIXED PLAN);'
	 ,N'@V_SpCode VARCHAR(16)',@V_SpCode = @SpCode;
GO
--------------------------------------------------------------------------------------------------
IF EXISTS(SELECT NAME FROM DBO.SYSOBJECTS WHERE NAME = 'Usp_Get_RecapDPRD_National' AND TYPE = 'P')
DROP PROCEDURE Usp_Get_RecapDPRD_National
GO
CREATE PROCEDURE Usp_Get_RecapDPRD_National
@START_DATE SMALLDATETIME,
@END_DATE SMALLDATETIME,
@ReportType VARCHAR(5)
AS
SET NOCOUNT ON;
IF @ReportType = 'DPRDS' 
   BEGIN
      SELECT RC.SpCode AS PROGRAM_ID,RC.BrandCode AS BRAND_ID,BR.BRAND_NAME,REG.REGIONAL_AREA,RC.TerritoryCode AS TERRITORY_ID,
      TERR.TERRITORY_AREA,RC.BudgetTerritory AS BUDGET_TERRITORY,RC.BudgetDispro AS BUDGET_DISPRO,
      RC.TotalCoverage AS TOTAL_SPKIOS,((RC.TotalCoverage/RC.BudgetTerritory)*100)/100 AS [% COVERAGE],
      RC.TotalActual AS TOTAL_ACTUAL,((RC.TotalActual/RC.TotalCoverage) * 100)/ 100 AS [% ACHIEVEMENT],RC.TotalDisc AS TOTAL_DISC,
      RC.StartDate AS START_DATE,RC.EndDate AS END_DATE
      FROM DIST_REGIONAL REG INNER JOIN TERRITORY TERR ON REG.REGIONAL_ID = TERR.REGIONAL_ID
      INNER JOIN RecapNDPRD RC ON TERR.TERRITORY_ID = RC.TerritoryCode
      INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = RC.BrandCode
      WHERE RC.StartDate >= @START_DATE AND RC.EndDate <= @END_DATE ;
   END
ELSE 
   BEGIN
      SELECT RC.SpCode AS PROGRAM_ID,RC.BrandCode AS BRAND_ID,BR.BRAND_NAME,REG.REGIONAL_AREA,RC.TerritoryCode AS TERRITORY_ID,
      TERR.TERRITORY_AREA,RC.BudgetTerritory AS BUDGET_TERRITORY,RC.BudgetDispro AS BUDGET_DISPRO,
      RC.TotalCoverage AS TOTAL_SPKIOS,((RC.TotalCoverage/RC.BudgetTerritory)*100)/100 AS [% COVERAGE],
      RC.TotalActual AS TOTAL_ACTUAL,((RC.TotalActual/RC.TotalCoverage) * 100)/ 100 AS [% ACHIEVEMENT],
      RC.TotalBonus AS TOTAL_BONUS,RC.StartDate AS START_DATE,RC.EndDate AS END_DATE
      FROM DIST_REGIONAL REG INNER JOIN TERRITORY TERR ON REG.REGIONAL_ID = TERR.REGIONAL_ID
      INNER JOIN RecapDPRDM RC ON TERR.TERRITORY_ID = RC.TerritoryCode
      INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = RC.BrandCode
      WHERE RC.StartDate >= @START_DATE AND RC.EndDate <= @END_DATE ;
   END
GO
--EXEC Usp_Get_RecapDPRD_National
--@START_DATE = '10/01/2010',
--@END_DATE = '05/30/2010',
--@ReportType = 'DPRDM'
------------------------------------------------------------------------------
IF EXISTS(SELECT NAME FROM DBO.SYSOBJECTS WHERE NAME = 'Usp_Get_Summary_RecapDPRD_National' AND TYPE = 'P')
DROP PROCEDURE Usp_Get_Summary_RecapDPRD_National
GO
CREATE PROCEDURE Usp_Get_Summary_RecapDPRD_National
@ReportType VARCHAR(20),
@START_DATE SMALLDATETIME,
@END_DATE SMALLDATETIME
AS
SET NOCOUNT ON;
IF @ReportType = 'DPRDS'
   BEGIN
     SELECT BR.BRAND_NAME,RCS.TOTAL_BUDGET,RCS.TOTAL_COVERAGE AS TOTAL_SPKIOS,((RCS.TOTAL_COVERAGE/RCS.TOTAL_BUDGET) * 100)/100 AS [% COVERAGE],
     RCS.TOTAL_ACTUAL,((RCS.TOTAL_ACTUAL/RCS.TOTAL_COVERAGE) * 100)/100 AS [% ACHIEVEMENT],
     RCS.TOTAL_DISC,BG.BUDGET_DISPRO
     FROM(SELECT BrandCode AS BRAND_ID,ISNULL(SUM(BudgetTerritory),0) AS TOTAL_BUDGET,
     	  ISNULL(SUM(TotalCoverage),0) AS TOTAL_COVERAGE,ISNULL(SUM(TotalActual),0) AS TOTAL_ACTUAL,
     	  ISNULL(SUM(TotalDisc),0) AS TOTAL_DISC
     	  FROM RecapNDPRD WHERE StartDate >= @START_DATE AND EndDate <= @END_DATE
     	  GROUP BY BrandCode
          )RCS
     INNER JOIN(
                SELECT DISTINCT BrandCode AS BRAND_ID,BudgetDispro AS BUDGET_DISPRO
     	        FROM RecapNDPRD WHERE StartDate >= @START_DATE  AND EndDate <= @END_DATE 
    	       )BG
     ON BG.BRAND_ID = RCS.BRAND_ID
     INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = BG.BRAND_ID AND BR.BRAND_ID = RCS.BRAND_ID
   END
ELSE 
    BEGIN
     SELECT BR.BRAND_NAME,RCS.TOTAL_BUDGET,RCS.TOTAL_COVERAGE AS TOTAL_SPKIOS,((RCS.TOTAL_COVERAGE/RCS.TOTAL_BUDGET) * 100)/100 AS [% COVERAGE],
     RCS.TOTAL_ACTUAL,((RCS.TOTAL_ACTUAL/RCS.TOTAL_COVERAGE) * 100)/100 AS [% ACHIEVEMENT],
     RCS.TOTAL_BONUS
     FROM(SELECT BrandCode AS BRAND_ID,ISNULL(SUM(BudgetTerritory),0) AS TOTAL_BUDGET,
     	  ISNULL(SUM(TotalCoverage),0) AS TOTAL_COVERAGE,ISNULL(SUM(TotalActual),0) AS TOTAL_ACTUAL,
     	  ISNULL(SUM(TotalBonus),0) AS TOTAL_BONUS
     	  FROM RecapDPRDM WHERE StartDate >= @START_DATE AND EndDate <= @END_DATE
     	  GROUP BY BrandCode
          )RCS
    INNER JOIN BRND_BRAND BR ON BR.BRAND_ID = RCS.BRAND_ID
   END 
GO

--EXEC Usp_Get_Summary_RecapDPRD_National
--@START_DATE = '10/01/2010',
--@END_DATE = '05/31/2011',
--@ReportType = 'DPRDS'

SELECT * FROM ORDR_PURCHASE_ORDER WHERE PO_REF_NO LIKE '%PO-SAM-CS%'
AND YEAR(PO_REF_DATE) >= 2010

  SELECT DISTINCT BrandCode AS BRAND_ID,BudgetDispro AS BUDGET_DISPRO,CreatedBy
  FROM RecapNDPRD WHERE StartDate >= '10/01/2010'  AND EndDate <= '05/31/2011' 


SELECT * FROM RecapNDPRD WHERE BrandCode = '00601'