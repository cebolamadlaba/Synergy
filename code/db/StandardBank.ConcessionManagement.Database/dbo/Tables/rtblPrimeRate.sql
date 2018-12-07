CREATE TABLE [dbo].[rtblPrimeRate] (
    [PRM_ID]          INT        IDENTITY (1, 1) NOT NULL,
    [PRM_Prime_Rate]  FLOAT (53) NULL,
    [PRM_Active_From] DATETIME   NULL,
    CONSTRAINT [PK_rtblPrimeRate] PRIMARY KEY CLUSTERED ([PRM_ID] ASC)
);

