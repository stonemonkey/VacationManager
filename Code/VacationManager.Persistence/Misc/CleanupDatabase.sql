
PRINT 'USE [' + db_name() +']';

/***** BEGIN - DROP non-sys PROCs *****/
DECLARE @schema NVARCHAR(128), @object_name NVARCHAR(128)
DECLARE @sqlCommand NVARCHAR(1000)

	DECLARE cursor_procs CURSOR FOR	
		SELECT		r.ROUTINE_SCHEMA, r.SPECIFIC_NAME
		FROM		INFORMATION_SCHEMA.ROUTINES r
		WHERE		r.ROUTINE_TYPE = 'PROCEDURE'
		ORDER BY	r.ROUTINE_SCHEMA, r.ROUTINE_NAME	
	OPEN cursor_procs
		FETCH NEXT FROM cursor_procs 
		INTO	@schema, @object_name
		WHILE	@@FETCH_STATUS = 0
			BEGIN
					SET		@sqlCommand = 'DROP PROC [' + @schema + '].[' + @object_name + '];'
					EXEC	sp_executesql	@sqlCommand;        
					
					PRINT	'Dropped PROC: [' + @schema + '].[' + @object_name + ']';
					
					FETCH NEXT FROM cursor_procs
					INTO @schema, @object_name
			END	
	CLOSE cursor_procs
	DEALLOCATE cursor_procs
GO
/***** END - DROP non-sys PROCs *****/

/***** BEGIN - DROP FUNCTIONs *****/
DECLARE @schema NVARCHAR(128), @object_name NVARCHAR(128)
DECLARE @sqlCommand NVARCHAR(1000)

	DECLARE cursor_functions CURSOR FOR	
		SELECT		r.ROUTINE_SCHEMA, r.SPECIFIC_NAME
		FROM		INFORMATION_SCHEMA.ROUTINES r
		WHERE		r.ROUTINE_TYPE = 'FUNCTION'
		ORDER BY	r.ROUTINE_SCHEMA, r.ROUTINE_NAME	
	OPEN cursor_functions
		FETCH NEXT FROM cursor_functions 
		INTO	@schema, @object_name
		WHILE	@@FETCH_STATUS = 0
			BEGIN
					SET		@sqlCommand = 'DROP FUNCTION [' + @schema + '].[' + @object_name + '];'
					EXEC	sp_executesql	@sqlCommand;        
					
					PRINT	'Dropped FUNCTION: [' + @schema + '].[' + @object_name + ']';
					
					FETCH NEXT FROM cursor_functions 
					INTO @schema, @object_name
			END	
	CLOSE cursor_functions
	DEALLOCATE cursor_functions
GO
/***** END - DROP FUNCTIONs *****/

/***** BEGIN - DROP VIEWs *****/
DECLARE @schema NVARCHAR(128), @object_name NVARCHAR(128)
DECLARE @sqlCommand NVARCHAR(1000)

	DECLARE cursor_views CURSOR FOR	
		SELECT		t.TABLE_SCHEMA, t.TABLE_NAME
		FROM		INFORMATION_SCHEMA.TABLES t
		WHERE		t.TABLE_TYPE = 'VIEW'
		ORDER BY	t.TABLE_SCHEMA, t.TABLE_NAME
	OPEN cursor_views
		FETCH NEXT FROM cursor_views 
		INTO	@schema, @object_name
		WHILE	@@FETCH_STATUS = 0
			BEGIN
					SET		@sqlCommand = 'DROP VIEW [' + @schema + '].[' + @object_name + '];'
					EXEC	sp_executesql	@sqlCommand;        
					
					PRINT	'Dropped VIEW: [' + @schema + '].[' + @object_name + ']';
					
					FETCH NEXT FROM cursor_views 
					INTO @schema, @object_name
			END	
	CLOSE cursor_views
	DEALLOCATE cursor_views
GO
/***** END - DROP VIEWs *****/

/***** BEGIN - DROP CONSTRAINTs *****/
/***** BEGIN - DROP FOREIGN KEY CONSTRAINTs *****/
DECLARE @schema NVARCHAR(128), @object_name NVARCHAR(128)
DECLARE @table_schema VARCHAR(128), @table_name VARCHAR(128)
DECLARE @sqlCommand NVARCHAR(1000)

	DECLARE cursor_foreign_key_constraints CURSOR FOR	
		SELECT		tc.CONSTRAINT_SCHEMA, tc.CONSTRAINT_NAME, tc.TABLE_SCHEMA, tc.TABLE_NAME
		FROM		INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc
		WHERE		tc.CONSTRAINT_CATALOG = DB_NAME() AND
					tc.CONSTRAINT_TYPE = 'FOREIGN KEY'
		ORDER BY	tc.TABLE_SCHEMA, tc.TABLE_NAME, tc.CONSTRAINT_SCHEMA, tc.CONSTRAINT_NAME
	OPEN cursor_foreign_key_constraints
		FETCH NEXT FROM cursor_foreign_key_constraints 
		INTO	@schema, @object_name, @table_schema, @table_name
		WHILE	@@FETCH_STATUS = 0
			BEGIN
					SET		@sqlCommand = 'ALTER TABLE  [' + @table_schema + '].[' + @table_name + '] DROP CONSTRAINT [' + @object_name + '];';
					EXEC	sp_executesql	@sqlCommand;        
					
					PRINT	'Dropped FOREIGN KEY CONSTRAINT: ' + @object_name + ' on [' + @table_schema + '].[' + @table_name + ']';
					
					FETCH NEXT FROM cursor_foreign_key_constraints 
					INTO @schema, @object_name, @table_schema, @table_name
			END	
	CLOSE cursor_foreign_key_constraints
	DEALLOCATE cursor_foreign_key_constraints
GO
/***** END - DROP FOREIGN KEY CONSTRAINTs *****/

/***** BEGIN - DROP PRIMARY KEY CONSTRAINTs *****/
DECLARE @schema NVARCHAR(128), @object_name NVARCHAR(128)
DECLARE @table_schema VARCHAR(128), @table_name VARCHAR(128)
DECLARE @sqlCommand NVARCHAR(1000)

	DECLARE cursor_primary_key_constraints CURSOR FOR	
		SELECT		tc.CONSTRAINT_SCHEMA, tc.CONSTRAINT_NAME, tc.TABLE_SCHEMA, tc.TABLE_NAME
		FROM		INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc
		WHERE		tc.CONSTRAINT_CATALOG = DB_NAME() AND
					tc.CONSTRAINT_TYPE = 'PRIMARY KEY'
		ORDER BY	tc.TABLE_SCHEMA, tc.TABLE_NAME, tc.CONSTRAINT_SCHEMA, tc.CONSTRAINT_NAME
	OPEN cursor_primary_key_constraints
		FETCH NEXT FROM cursor_primary_key_constraints 
		INTO	@schema, @object_name, @table_schema, @table_name
		WHILE	@@FETCH_STATUS = 0
			BEGIN
					SET		@sqlCommand = 'ALTER TABLE  [' + @table_schema + '].[' + @table_name + '] DROP CONSTRAINT [' + @object_name + '];';
					EXEC	sp_executesql	@sqlCommand;        
					
					PRINT	'Dropped PRIMARY KEY CONSTRAINT: ' + @object_name + ' on [' + @table_schema + '].[' + @table_name + ']';
					
					FETCH NEXT FROM cursor_primary_key_constraints 
					INTO @schema, @object_name, @table_schema, @table_name
			END	
	CLOSE cursor_primary_key_constraints
	DEALLOCATE cursor_primary_key_constraints
GO
/***** END - DROP PRIMARY KEY CONSTRAINTs *****/
/***** END - DROP CONSTRAINTs *****/

/***** BEGIN - DROP TABLEs *****/
DECLARE @schema NVARCHAR(128), @object_name NVARCHAR(128)
DECLARE @sqlCommand NVARCHAR(1000)

	DECLARE cursor_tables CURSOR FOR	
		SELECT		t.TABLE_SCHEMA, t.TABLE_NAME
		FROM		INFORMATION_SCHEMA.TABLES t
		WHERE		t.TABLE_TYPE = 'BASE TABLE'
		ORDER BY	t.TABLE_SCHEMA, t.TABLE_NAME
	OPEN cursor_tables
		FETCH NEXT FROM cursor_tables 
		INTO	@schema, @object_name
		WHILE	@@FETCH_STATUS = 0
			BEGIN
					SET		@sqlCommand = 'DROP TABLE [' + @schema + '].[' + @object_name + '];'
					EXEC	sp_executesql	@sqlCommand;        
					
					PRINT	'Dropped TABLE: [' + @schema + '].[' + @object_name + ']';
					
					FETCH NEXT FROM cursor_tables 
					INTO @schema, @object_name
			END	
	CLOSE cursor_tables
	DEALLOCATE cursor_tables
GO
/***** END - DROP TABLEs *****/

/***** BEGIN - DROP User-Defined Data Types *****/
DECLARE @schema NVARCHAR(128), @object_name NVARCHAR(128)
DECLARE @sqlCommand NVARCHAR(1000)

	DECLARE cursor_user_types CURSOR FOR 
		SELECT	S.name AS SchemaName, T.name AS TypeName 
		FROM	sys.types	T inner join
				sys.schemas S on S.schema_id = T.schema_id 
		WHERE	is_user_defined = 1 
	OPEN cursor_user_types 
	FETCH NEXT FROM cursor_user_types INTO @schema, @object_name
	WHILE @@FETCH_STATUS = 0 
		BEGIN 
			SET @sqlCommand = 'DROP TYPE ' + @schema + '.' + @object_name 
			EXEC sp_executesql @sqlCommand
			
			PRINT 'Dropped User-Defined Type: [' + @schema + '].[' + @object_name + ']';    
			
			FETCH NEXT FROM cursor_user_types INTO @schema, @object_name 
		END 
	CLOSE cursor_user_types 
	DEALLOCATE cursor_user_types
GO
/***** END - DROP User-Defined Data Types *****/

/***** BEGIN - DROP Schemas *****/
DECLARE @object_name NVARCHAR(128)
DECLARE @sqlCommand NVARCHAR(1000)

	DECLARE cursor_schemas CURSOR FOR 
		SELECT	S.name AS SchemaName
		FROM	sys.schemas S
		WHERE	S.name IN ('audit', 'auth', 'context', 'core', 'ref', 'std')
	OPEN cursor_schemas 
	FETCH NEXT FROM cursor_schemas INTO @object_name
	WHILE @@FETCH_STATUS = 0 
		BEGIN 
			SET @sqlCommand = 'DROP SCHEMA [' + @object_name + ']'
			EXEC sp_executesql @sqlCommand
			
			PRINT 'Dropped Schema: [' + @object_name + ']';    
			
			FETCH NEXT FROM cursor_schemas INTO @object_name 
		END 
	CLOSE cursor_schemas 
	DEALLOCATE cursor_schemas
GO
/***** END - DROP Schemas *****/