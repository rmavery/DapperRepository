
SET NUMERIC_ROUNDABORT OFF
GO
SET ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO
BEGIN TRANSACTION
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[lit_hold_users]'
GO
CREATE TABLE [dbo].[lit_hold_users]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[display_name] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[email_address] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_lit_hold_users] on [dbo].[lit_hold_users]'
GO
ALTER TABLE [dbo].[lit_hold_users] ADD CONSTRAINT [PK_lit_hold_users] PRIMARY KEY CLUSTERED  ([id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[lit_hold_users_xref]'
GO
CREATE TABLE [dbo].[lit_hold_users_xref]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[lit_hold_id] [int] NOT NULL,
[lit_hold_user_id] [int] NOT NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_lit_hold_users_xref] on [dbo].[lit_hold_users_xref]'
GO
ALTER TABLE [dbo].[lit_hold_users_xref] ADD CONSTRAINT [PK_lit_hold_users_xref] PRIMARY KEY CLUSTERED  ([id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating [dbo].[lit_holds]'
GO
CREATE TABLE [dbo].[lit_holds]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[work_order] [varchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[matter_no] [varchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[case_name] [varchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[begin_date] [datetime] NULL,
[end_date] [datetime] NULL,
[notes] [varchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Creating primary key [PK_lit_holds] on [dbo].[lit_holds]'
GO
ALTER TABLE [dbo].[lit_holds] ADD CONSTRAINT [PK_lit_holds] PRIMARY KEY CLUSTERED  ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
PRINT N'Adding foreign keys to [dbo].[lit_hold_users_xref]'
GO
ALTER TABLE [dbo].[lit_hold_users_xref] ADD CONSTRAINT [FK_lit_hold_users_xref_lit_hold_users] FOREIGN KEY ([lit_hold_user_id]) REFERENCES [dbo].[lit_hold_users] ([id])
GO
ALTER TABLE [dbo].[lit_hold_users_xref] ADD CONSTRAINT [FK_lit_hold_users_xref_lit_hold_users_xref] FOREIGN KEY ([id]) REFERENCES [dbo].[lit_hold_users_xref] ([id])
GO
ALTER TABLE [dbo].[lit_hold_users_xref] ADD CONSTRAINT [FK_lit_hold_users_xref_lit_holds] FOREIGN KEY ([lit_hold_id]) REFERENCES [dbo].[lit_holds] ([Id])
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
COMMIT TRANSACTION
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
DECLARE @Success AS BIT
SET @Success = 1
SET NOEXEC OFF
IF (@Success = 1) PRINT 'The database update succeeded'
ELSE BEGIN
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
	PRINT 'The database update failed'
END
GO
