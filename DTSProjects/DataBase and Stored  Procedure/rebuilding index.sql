USE Nufarm

BEGIN

DECLARE @TableName sysname
DECLARE cur_showfragmentation CURSOR FOR
SELECT table_name FROM information_schema.tables WHERE table_type = 'base table'
OPEN cur_showfragmentation
FETCH NEXT FROM cur_showfragmentation INTO @TableName
WHILE @@FETCH_STATUS = 0
BEGIN
  PRINT 'Show fragmentation for the ' + CAST((@TableName)AS VARCHAR(30)) + ' table'
  DBCC SHOWCONTIG (@TableName)
  FETCH NEXT FROM cur_showfragmentation INTO @TableName
END
CLOSE cur_showfragmentation
DEALLOCATE cur_showfragmentation


END

--DBCC DBREINDEX(ORDR_OA_BRANDPACK,'',80)
--DBCC INDEXDEFRAG(Nufarm,DISTRIBUTOR_AGREEMENT,IX_DISTRIBUTOR_AGREEMENT_1)
--DBCC SHOWCONTIG (ORDR_OA_BRANDPACK)

EXECUTE SP_EXECUTESQL
N'
BEGIN
	DECLARE @TableName sysname
	DECLARE cur_reindex CURSOR FOR
	SELECT table_name
	  FROM information_schema.tables
	  WHERE table_type = ''base table''
	OPEN cur_reindex
	FETCH NEXT FROM cur_reindex INTO @TableName
	WHILE @@FETCH_STATUS = 0
	BEGIN
	  PRINT ''Reindexing '' + @TableName + '' table''
	  DBCC DBREINDEX (@TableName, ''  '', 80)
	  FETCH NEXT FROM cur_reindex INTO @TableName
	END
	CLOSE cur_reindex
        DEALLOCATE cur_reindex
END
'



--DBCC CHECKDB('msdb',REPAIR_ALLOW_DATA_LOSS)--must be in single user mode before

DBCC CHECKDB('Nufarm')