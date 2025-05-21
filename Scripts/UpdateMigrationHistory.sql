-- Check if the migration history table exists
IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NOT NULL
BEGIN
    -- Check if the migration entry already exists
    IF NOT EXISTS (SELECT 1 FROM [__EFMigrationsHistory] WHERE [MigrationId] = '20250521110701_CreateParticipantsTable')
    BEGIN
        -- Add the migration entry
        INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
        VALUES ('20250521110701_CreateParticipantsTable', '8.0.0');
        
        PRINT 'Migration 20250521110701_CreateParticipantsTable added to __EFMigrationsHistory table.';
    END
    ELSE
    BEGIN
        PRINT 'Migration 20250521110701_CreateParticipantsTable already exists in __EFMigrationsHistory table.';
    END
END
ELSE
BEGIN
    PRINT 'The __EFMigrationsHistory table does not exist.';
END 