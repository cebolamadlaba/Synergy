select ist.TABLE_SCHEMA + ';' + ist.TABLE_NAME + ';' + isc.COLUMN_NAME + ';' + isc.IS_NULLABLE + ';' + isc.DATA_TYPE from INFORMATION_SCHEMA.TABLES ist
join INFORMATION_SCHEMA.COLUMNS isc on ist.TABLE_SCHEMA = isc.TABLE_SCHEMA and ist.TABLE_NAME = isc.TABLE_NAME
where ist.TABLE_SCHEMA = 'dbo' and ist.TABLE_NAME not in ('''Autorizing Users$''', 'changelog', 'SMTRawData', 'tblExceptionLog')
ORDER BY ist.TABLE_SCHEMA, ist.TABLE_NAME, isc.ORDINAL_POSITION
