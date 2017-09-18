select 'public decimal ' + COLUMN_NAME + ' { get; set; }' from INFORMATION_SCHEMA.COLUMNS
where TABLE_NAME = 'tblProductTransactional'

SELECT LOWER(LEFT(COLUMN_NAME, 1)) + SUBSTRING(COLUMN_NAME, 2, LEN(COLUMN_NAME) - 1) + ': number;' from INFORMATION_SCHEMA.COLUMNS
where TABLE_NAME = 'tblProductTransactional'