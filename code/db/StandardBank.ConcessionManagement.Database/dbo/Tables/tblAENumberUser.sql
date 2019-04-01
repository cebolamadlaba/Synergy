CREATE TABLE [dbo].[tblAENumberUser] (
    [pkAENumberUserId] INT          IDENTITY (1, 1) NOT NULL,
    [AENumber]         VARCHAR (25) NOT NULL,
    [fkUserId]         INT          NOT NULL,
    [IsActive]         BIT          DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([pkAENumberUserId] ASC),
    FOREIGN KEY ([fkUserId]) REFERENCES [dbo].[tblUser] ([pkUserId])
);

