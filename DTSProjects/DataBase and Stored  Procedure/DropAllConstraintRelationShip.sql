-- Helper Procedure 
CREATE PROC #DropConstraints 
  @tableSchema nvarchar(max), 
  @tableName nvarchar(max), 
  @constraintType nvarchar(20) 
AS 
BEGIN 
  DECLARE @cName nvarchar(max); 

  DECLARE constraint_cursor CURSOR FOR 
    SELECT CONSTRAINT_NAME  
    FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS 
    WHERE  
      CONSTRAINT_TYPE = @constraintType 
      AND TABLE_NAME = @tableName 
      AND TABLE_SCHEMA = @tableSchema 

  OPEN constraint_cursor 

  FETCH NEXT FROM constraint_cursor INTO @cName 
  WHILE @@FETCH_STATUS = 0 
  BEGIN 
    EXEC ('ALTER TABLE ' + @tableSchema + '.' + @tableName + ' DROP CONSTRAINT ' + @cName); 
    FETCH NEXT FROM constraint_cursor INTO @cName 
  END 

  CLOSE constraint_cursor 
  DEALLOCATE constraint_cursor 
END 
GO

BEGIN TRANSACTION
DECLARE @tableSchema nvarchar(max), @tableName nvarchar(max);
  -- Setup Cursor for looping 
  DECLARE table_cursor SCROLL CURSOR FOR 
    SELECT TABLE_SCHEMA, TABLE_NAME  
    FROM INFORMATION_SCHEMA.TABLES 

  OPEN table_cursor

 -- Drop Primary Keys 
  FETCH FIRST FROM table_cursor INTO @tableSchema, @tableName 
  WHILE @@FETCH_STATUS = 0 
  BEGIN 
    EXEC #DropConstraints @tableSchema, @tableName, 'FOREIGN KEY'; 

    FETCH NEXT FROM table_cursor INTO @tableSchema, @tableName 
  END 

  -- Cleanup 
  CLOSE table_cursor 
  DEALLOCATE table_cursor

COMMIT TRANSACTION 
GO

DROP PROCEDURE #DropConstraints;
GO