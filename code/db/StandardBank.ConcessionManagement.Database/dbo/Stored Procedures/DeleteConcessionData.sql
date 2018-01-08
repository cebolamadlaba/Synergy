
CREATE PROCEDURE [dbo].[DeleteConcessionData]
	@AreYouSure int
AS

BEGIN

	IF (@AreYouSure = 1)
	BEGIN
		DELETE FROM [dbo].[tblConcessionApproval]

		DELETE FROM [dbo].[tblConcessionAccount]

		DELETE FROM [dbo].[tblConcessionCondition]

		DELETE FROM [dbo].[tblConcessionRelationship]

		DELETE FROM [dbo].[tblConcessionComment]

		DELETE FROM [dbo].[tblConcessionBol]

		DELETE FROM [dbo].[tblConcessionCash] 

		DELETE FROM [dbo].[tblConcessionInvestment]

		DELETE FROM [dbo].[tblConcessionLending]

		DELETE FROM [dbo].[tblConcessionMas]

		DELETE FROM [dbo].[tblConcessionTrade]

		DELETE FROM [dbo].[tblConcessionTransactional]

		DELETE FROM [dbo].[tblConcessionDetail]

		DELETE FROM [dbo].[tblConcession]

	END

END