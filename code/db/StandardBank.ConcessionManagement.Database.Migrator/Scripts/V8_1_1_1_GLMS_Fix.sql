

Alter Table tblConcessionGlms
Alter Column fkLegalEntityAccountId Int Null

Alter Table tblConcessionGlms
Alter Column fkProductId Int Null




Alter Table tblConcessionGlms
Drop Column	fkProductId

Declare @constraintName Varchar(250)
SELECT		@constraintName = 'Alter Table tblConcessionGlms Drop Constraint ' + KCU.CONSTRAINT_NAME
FROM		INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS AS RC 
INNER JOIN	INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KCU
			ON KCU.CONSTRAINT_CATALOG = RC.CONSTRAINT_CATALOG  
			AND KCU.CONSTRAINT_SCHEMA = RC.CONSTRAINT_SCHEMA 
			AND KCU.CONSTRAINT_NAME = RC.CONSTRAINT_NAME
Where		TABLE_NAME = 'tblConcessionGlms'
And			COLUMN_NAME = 'fkLegalEntityAccountId'
Order By	TABLE_NAME
exec(@constraintName)

Alter Table tblConcessionGlms
Drop Column	fkLegalEntityAccountId
Go

Declare @constraintName Varchar(250)
SELECT		@constraintName = 'Alter Table tblProductGlms Drop Constraint ' + KCU.CONSTRAINT_NAME
FROM		INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS AS RC 
INNER JOIN	INFORMATION_SCHEMA.KEY_COLUMN_USAGE AS KCU
			ON KCU.CONSTRAINT_CATALOG = RC.CONSTRAINT_CATALOG  
			AND KCU.CONSTRAINT_SCHEMA = RC.CONSTRAINT_SCHEMA 
			AND KCU.CONSTRAINT_NAME = RC.CONSTRAINT_NAME
Where		TABLE_NAME = 'tblProductGlms'
And			COLUMN_NAME = 'fkLegalEntityAccountId'
Order By	TABLE_NAME
exec(@constraintName)

Alter Table tblProductGlms
Drop Column	fkLegalEntityAccountId