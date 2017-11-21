CREATE TABLE [dbo].[Doctors] (
    [DoctorId]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [EmailAddress] NVARCHAR (MAX) NULL,
    [FirstName]    NVARCHAR (MAX) NULL,
    [LastName]     NVARCHAR (MAX) NULL,
    [PhoneNumber]  NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Doctors] PRIMARY KEY CLUSTERED ([DoctorId] ASC)
);

