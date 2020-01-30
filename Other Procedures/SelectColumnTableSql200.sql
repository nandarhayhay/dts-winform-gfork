SELECT
clmns.name [Name] FROM
sysobjects  tbl INNER JOIN syscolumns clmns
ON clmns.id=tbl.id
WHERE
(tbl.name=N'AGREE_AGREEMENT')
ORDER BY clmns.colorder ASC