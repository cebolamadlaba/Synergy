CREATE TABLE [dbo].[tblCentreUser] (
    [pkCentreUserId] INT IDENTITY (1, 1) NOT NULL,
    [fkCentreId]     INT NOT NULL,
    [fkUserId]       INT NOT NULL,
    [IsActive]       BIT CONSTRAINT [DF_tblCentreUser_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tblCentreUser] PRIMARY KEY CLUSTERED ([pkCentreUserId] ASC),
    CONSTRAINT [FK_tblCentreUser_tblCentre] FOREIGN KEY ([fkCentreId]) REFERENCES [dbo].[tblCentre] ([pkCentreId]),
    CONSTRAINT [FK_tblCentreUser_tblUser] FOREIGN KEY ([fkUserId]) REFERENCES [dbo].[tblUser] ([pkUserId])
);

