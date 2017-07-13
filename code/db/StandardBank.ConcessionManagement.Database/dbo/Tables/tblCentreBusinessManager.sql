CREATE TABLE [dbo].[tblCentreBusinessManager] (
    [pkCentreBusinessManagerId] INT IDENTITY (1, 1) NOT NULL,
    [fkCentreId]                INT NULL,
    [fkUserId]                  INT NOT NULL,
    [IsActive]                  BIT CONSTRAINT [DF_tblCentreBusinessManager_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tblCentreBusinessManager] PRIMARY KEY CLUSTERED ([pkCentreBusinessManagerId] ASC),
    CONSTRAINT [FK_tblCentreBusinessManager_tblCentre] FOREIGN KEY ([fkCentreId]) REFERENCES [dbo].[tblCentre] ([pkCentreId]),
    CONSTRAINT [FK_tblCentreBusinessManager_tblUser] FOREIGN KEY ([fkUserId]) REFERENCES [dbo].[tblUser] ([pkUserId])
);

