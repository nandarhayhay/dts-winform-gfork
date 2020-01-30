SELECT
clmns.name [Name] FROM
sys.tables  tbl INNER JOIN sys.all_columns clmns
ON clmns.object_id=tbl.object_id
WHERE
(tbl.name=N'Agreement' and SCHEMA_NAME(tbl.schema_id)=N'dbo')
ORDER BY clmns.column_id ASC