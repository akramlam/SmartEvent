-- Create Participants table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Participants]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[Participants](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        [EventId] [int] NOT NULL,
        [Name] [nvarchar](100) NOT NULL,
        [Email] [nvarchar](100) NOT NULL,
        [RegistrationDate] [datetime2](7) NOT NULL,
        [HasAttended] [bit] NOT NULL,
        [RegistrationCode] [nvarchar](max) NOT NULL,
        CONSTRAINT [PK_Participants] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [FK_Participants_Events_EventId] FOREIGN KEY([EventId]) REFERENCES [dbo].[Events] ([Id]) ON DELETE CASCADE
    );

    -- Create unique index to prevent duplicate registrations
    CREATE UNIQUE NONCLUSTERED INDEX [IX_Participants_EventId_Email] ON [dbo].[Participants]
    (
        [EventId] ASC,
        [Email] ASC
    );

    PRINT 'Participants table created successfully.';
END
ELSE
BEGIN
    PRINT 'Participants table already exists.';
END 