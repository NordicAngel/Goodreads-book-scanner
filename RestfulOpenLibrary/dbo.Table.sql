CREATE TABLE [dbo].[Books_In_List1] (
    [ID]      INT IDENTITY (1, 1) NOT NULL,
    [List_ID] INT NOT NULL,
    [ISBN]    INT NOT NULL,
    CONSTRAINT [PK_Books_In_List1] PRIMARY KEY CLUSTERED ([ID] ASC)
);
