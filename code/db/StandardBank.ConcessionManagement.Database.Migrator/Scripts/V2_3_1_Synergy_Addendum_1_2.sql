Begin Transaction

	Update	tblConcessionCondition
	Set		Value = ExpectedTurnoverValue
	Where	Value Is Null
	And		ExpectedTurnoverValue Is Not Null

	Select	Value, ExpectedTurnoverValue
	From	tblConcessionCondition

Rollback
Commit