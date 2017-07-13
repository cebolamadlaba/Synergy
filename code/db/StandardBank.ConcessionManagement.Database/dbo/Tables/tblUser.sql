﻿CREATE TABLE [dbo].[tblUser] (
    [pkUserId]     INT            IDENTITY (1, 1) NOT NULL,
    [ANumber]      NVARCHAR (10)  NOT NULL,
    [EmailAddress] NVARCHAR (500) NOT NULL,
    [FirstName]    NVARCHAR (500) NOT NULL,
    [Surname]      NVARCHAR (500) NOT NULL,
    [IsActive]     BIT            CONSTRAINT [DF_tblUser_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tblUser] PRIMARY KEY CLUSTERED ([pkUserId] ASC)
);

