CREATE TABLE [dbo].[Medications] (
    [MedicationId]  NVARCHAR (450) NOT NULL,
    [DosageInMg]    REAL           NOT NULL,
    [PlaintextName] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Medications] PRIMARY KEY CLUSTERED ([MedicationId] ASC)
);

