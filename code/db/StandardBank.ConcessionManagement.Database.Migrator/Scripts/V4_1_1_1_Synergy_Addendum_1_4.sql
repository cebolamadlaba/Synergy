

Alter Table tblLegalEntity
Alter Column fkRiskGroupId Int Null
Go

-- Add tblConcessionLending.MRS_BRI and populate value from tblConcession.MRS_CRS
Begin Transaction

    Alter Table tblConcessionLending
    Add MRS_BRI Int Not Null Default(0)
    GO

    Update		cl
    Set			cl.MRS_BRI = RIGHT(SUBSTRING(Cast(MRS_CRS As Varchar(16)),0,CHARINDEX('.', Cast(MRS_CRS As Varchar(16)))),2)
    From		tblConcessionLending cl
    Inner Join	tblConcession c			On	c.pkConcessionId	=	cl.fkConcessionId
    GO

    Select	pkConcessionId, MRS_CRS
		    , Cast(MRS_CRS As Varchar(16))
		    , SUBSTRING(Cast(MRS_CRS As Varchar(16)),0,CHARINDEX('.', Cast(MRS_CRS As Varchar(16))))
		    , RIGHT(SUBSTRING(Cast(MRS_CRS As Varchar(16)),0,CHARINDEX('.', Cast(MRS_CRS As Varchar(16)))),2) [Concession_Value]
		    , cl.MRS_BRI
    From	tblConcession c
    Inner Join	tblConcessionLending cl	On	cl.fkConcessionId	=	c.pkConcessionId

Rollback
Commit

Alter Table tblConcession
Alter Column fkRiskGroupId Int Null
Go

Alter Table tblConcession
Add fkLegalEntityId Int Null Foreign Key (fkLegalEntityId) References tblLegalEntity(pkLegalEntityId)
Go