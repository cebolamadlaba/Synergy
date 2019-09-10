
Create Table tblMarketSegmentEnablementTeam
(
	pkMarketSegmentEnablementTeamId Int Identity(1,1) Not Null Primary Key,
	fkMarketSegmentId Int Not Null Foreign Key (fkMarketSegmentId) References rtblMarketSegment(pkMarketSegmentId),
	EnablementTeamUserEmail Varchar(500) Not Null,
	IsActive Bit Not Null Default(1)
)