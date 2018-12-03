

Create Table tblAENumberUser
(
	pkAENumberUserId Int Identity(1,1) Not Null Primary Key,
	AENumber Varchar(25) Not Null,
	fkUserId Int Not Null Foreign Key (fkUserId) References tblUser(pkUserId),
	IsActive Bit Not Null Default(1)
)

Alter Table tblConcession
Add fkAENumberUserId Int Null Foreign Key (fkAENumberUserId) References tblAENumberUser(pkAENumberUserId)

Begin Transaction
	Update		c
	Set			c.fkAENumberUserId	=	au.pkAENumberUserId
	From		tblConcession c
	Inner Join	tblAENumberUser au	On	au.fkUserId = c.fkRequestorId
Rollback
Commit

--Alter Table tblConcession
--Alter Column fkAENumberUserId Int Not Null Foreign Key (fkAENumberUserId) References tblAENumberUser(pkAENumberUserId)