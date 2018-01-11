CREATE TABLE [dbo].[tblAccountExecutiveAssistant] (
    [pkAccountExecutiveAssistantId] INT IDENTITY (1, 1) NOT NULL,
    [fkAccountAssistantUserId]      INT NOT NULL,
    [fkAccountExecutiveUserId]      INT NOT NULL,
    [IsActive]                      BIT CONSTRAINT [DF_tblAccountExecutiveAssistant_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tblAccountExecutiveAssistant] PRIMARY KEY CLUSTERED ([pkAccountExecutiveAssistantId] ASC),
    CONSTRAINT [FK_tblAccountExecutiveAssistant_tblUser_AA] FOREIGN KEY ([fkAccountAssistantUserId]) REFERENCES [dbo].[tblUser] ([pkUserId]),
    CONSTRAINT [FK_tblAccountExecutiveAssistant_tblUser_AE] FOREIGN KEY ([fkAccountExecutiveUserId]) REFERENCES [dbo].[tblUser] ([pkUserId])
);

