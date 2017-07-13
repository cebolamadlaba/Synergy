CREATE TABLE [dbo].[tblBolUser] (
    [pkBolUserId] INT           IDENTITY (1, 1) NOT NULL,
    [UserName]    VARCHAR (250) NOT NULL,
    [IsActive]    BIT           NOT NULL,
    CONSTRAINT [PK_tblBolUser] PRIMARY KEY CLUSTERED ([pkBolUserId] ASC)
);

