

Alter Table rtblSubRole
Add fkRoleId Int Null Foreign Key (fkRoleId) References rtblRole(pkRoleId)

    Update		sr
    Set			sr.fkRoleId = r.pkRoleId
    from		rtblSubRole sr
    Inner Join	rtblRole r	On	r.RoleName = 'AA'
    Where		[Name] <> 'No-Subrole'


Insert Into rtblSubRole ([Name])
Values('PCM S&I')

    Update		sr
    Set			sr.fkRoleId = r.pkRoleId
    from		rtblSubRole sr
    Inner Join	rtblRole r	On	r.RoleName = 'PCM'
    Where		[Name] = 'PCM S&I'

-- Insert PCM S&I SubStatuses
Insert Into rtblSubStatus ([Description], [IsActive])
Values
	('PCM S&I Pending', 1), 
	('PCM S&I Approved', 1), 
	('PCM S&I Declined', 1)