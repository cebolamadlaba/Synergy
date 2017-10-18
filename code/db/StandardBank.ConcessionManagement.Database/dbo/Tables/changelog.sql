CREATE TABLE [dbo].[changelog] (
    [id]           INT           IDENTITY (1, 1) NOT NULL,
    [type]         TINYINT       NULL,
    [version]      VARCHAR (50)  NULL,
    [description]  VARCHAR (200) NOT NULL,
    [name]         VARCHAR (300) NOT NULL,
    [checksum]     VARCHAR (32)  NULL,
    [installed_by] VARCHAR (100) NOT NULL,
    [installed_on] DATETIME      DEFAULT (getdate()) NOT NULL,
    [success]      BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

