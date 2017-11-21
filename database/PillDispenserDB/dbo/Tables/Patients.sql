CREATE TABLE [dbo].[Patients] (
    [PatientId]    BIGINT         IDENTITY (1, 1) NOT NULL,
    [EmailAddress] NVARCHAR (MAX) NULL,
    [FirstName]    NVARCHAR (MAX) NULL,
    [LastName]     NVARCHAR (MAX) NULL,
    [PhoneNumber]  NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Patients] PRIMARY KEY CLUSTERED ([PatientId] ASC)
);

