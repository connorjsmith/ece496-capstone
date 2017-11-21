CREATE TABLE [dbo].[PatientDoctor] (
    [PatientId] BIGINT NOT NULL,
    [DoctorId]  BIGINT NOT NULL,
    CONSTRAINT [PK_PatientDoctor] PRIMARY KEY CLUSTERED ([PatientId] ASC, [DoctorId] ASC),
    CONSTRAINT [FK_PatientDoctor_Doctors_DoctorId] FOREIGN KEY ([DoctorId]) REFERENCES [dbo].[Doctors] ([DoctorId]) ON DELETE CASCADE,
    CONSTRAINT [FK_PatientDoctor_Patients_PatientId] FOREIGN KEY ([PatientId]) REFERENCES [dbo].[Patients] ([PatientId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_PatientDoctor_DoctorId]
    ON [dbo].[PatientDoctor]([DoctorId] ASC);

