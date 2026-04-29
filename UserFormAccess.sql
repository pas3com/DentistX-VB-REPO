USE [DentistX];
GO

/* Per-user access to registered forms (see Forms table). Admins (group name ADMINS) bypass in app. */
IF OBJECT_ID(N'dbo.UserFormAccess', N'U') IS NULL
BEGIN
    CREATE TABLE [dbo].[UserFormAccess](
        [UsID] [int] NOT NULL,
        [FormID] [int] NOT NULL,
        [IsAllowed] [bit] NOT NULL CONSTRAINT [DF_UserFormAccess_IsAllowed] DEFAULT ((1)),
     CONSTRAINT [PK_UserFormAccess] PRIMARY KEY CLUSTERED ([UsID] ASC, [FormID] ASC));

    ALTER TABLE [dbo].[UserFormAccess]  WITH CHECK ADD  CONSTRAINT [FK_UserFormAccess_Users] FOREIGN KEY([UsID])
    REFERENCES [dbo].[Users] ([UsID]);

    ALTER TABLE [dbo].[UserFormAccess] CHECK CONSTRAINT [FK_UserFormAccess_Users];

    ALTER TABLE [dbo].[UserFormAccess]  WITH CHECK ADD  CONSTRAINT [FK_UserFormAccess_Forms] FOREIGN KEY([FormID])
    REFERENCES [dbo].[Forms] ([FormID]);

    ALTER TABLE [dbo].[UserFormAccess] CHECK CONSTRAINT [FK_UserFormAccess_Forms];
END
GO

/* Optional friendly title for the form-access grid (forms: prefer setting class DisplayName attribute; user controls: same, or edit Title in grid). */
IF COL_LENGTH(N'dbo.Forms', N'DisplayTitle') IS NULL
BEGIN
    ALTER TABLE [dbo].[Forms] ADD [DisplayTitle] [nvarchar](255) NULL;
END
GO

IF COL_LENGTH(N'dbo.Forms', N'DisplayTitleAr') IS NULL
BEGIN
    ALTER TABLE [dbo].[Forms] ADD [DisplayTitleAr] [nvarchar](255) NULL;
END
GO
