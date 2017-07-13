CREATE TABLE [dbo].[tblExceptionLog] (
    [ExceptionLogId]   INT           IDENTITY (1, 1) NOT NULL,
    [ExceptionMessage] VARCHAR (100) NULL,
    [ExceptionType]    VARCHAR (100) NULL,
    [ExceptionSource]  VARCHAR (MAX) NULL,
    [ExceptionData]    VARCHAR (MAX) NULL,
    [Logdate]          DATETIME      NULL,
    CONSTRAINT [PK_tblExceptionLog] PRIMARY KEY CLUSTERED ([ExceptionLogId] ASC)
);

