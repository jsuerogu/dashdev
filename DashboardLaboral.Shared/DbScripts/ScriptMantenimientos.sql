/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Empresa ADD
	Rowid uniqueidentifier NULL
GO
ALTER TABLE dbo.Empresa ADD CONSTRAINT
	DF_Empresa_Rowid DEFAULT newid() FOR Rowid
GO
ALTER TABLE dbo.Empresa SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

GO
UPDATE dbo.Empresa
Set RowId = newid()

GO
/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Parametros ADD
	RowId uniqueidentifier NULL
GO
ALTER TABLE dbo.Parametros ADD CONSTRAINT
	DF_Parametros_RowId DEFAULT newid() FOR RowId
GO
ALTER TABLE dbo.Parametros SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
GO
UPDATE dbo.Parametros
Set RowId = newid()
GO
/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Parametros ADD
	Mensaje varchar(250) NULL
GO
ALTER TABLE dbo.Parametros SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

GO

Update Ausentismo
set Autel = 0
where AUTEL is null

GO

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Ausentismo
	(
	ID nvarchar(10) NOT NULL,
	AUCOD nvarchar(10) NOT NULL,
	AUDES nchar(100) NOT NULL,
	AUJUS bit NOT NULL,
	AUTEL bit NOT NULL,
	RIESGO int NOT NULL,
	CUARENTENA int NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Ausentismo SET (LOCK_ESCALATION = TABLE)
GO
ALTER TABLE dbo.Tmp_Ausentismo ADD CONSTRAINT
	DF_Ausentismo_AUJUS DEFAULT 0 FOR AUJUS
GO
ALTER TABLE dbo.Tmp_Ausentismo ADD CONSTRAINT
	DF_Ausentismo_AUTEL DEFAULT 0 FOR AUTEL
GO
ALTER TABLE dbo.Tmp_Ausentismo ADD CONSTRAINT
	DF_Ausentismo_RIESGO DEFAULT 0 FOR RIESGO
GO
ALTER TABLE dbo.Tmp_Ausentismo ADD CONSTRAINT
	DF_Ausentismo_CUARENTENA DEFAULT 0 FOR CUARENTENA
GO
IF EXISTS(SELECT * FROM dbo.Ausentismo)
	 EXEC('INSERT INTO dbo.Tmp_Ausentismo (ID, AUCOD, AUDES, AUJUS, AUTEL, RIESGO, CUARENTENA)
		SELECT ID, AUCOD, AUDES, AUJUS, AUTEL, RIESGO, CUARENTENA FROM dbo.Ausentismo WITH (HOLDLOCK TABLOCKX)')
GO
DROP TABLE dbo.Ausentismo
GO
EXECUTE sp_rename N'dbo.Tmp_Ausentismo', N'Ausentismo', 'OBJECT' 
GO
COMMIT
GO
/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_ParametroCorreos
	(
	Id int NOT NULL IDENTITY (1, 1),
	Destinatario varchar(250) NULL,
	Empresa nvarchar(10) NULL,
	Vicepresidencia nvarchar(200) NULL,
	Departamento nvarchar(200) NULL,
	Indicadores nvarchar(1000) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_ParametroCorreos SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_ParametroCorreos ON
GO
IF EXISTS(SELECT * FROM dbo.ParametroCorreos)
	 EXEC('INSERT INTO dbo.Tmp_ParametroCorreos (Id, Destinatario, Empresa, Vicepresidencia, Departamento, Indicadores)
		SELECT Id, Destinatario, Empresa, Vicepresidencia, Departamento, Indicadores FROM dbo.ParametroCorreos WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_ParametroCorreos OFF
GO
DROP TABLE dbo.ParametroCorreos
GO
EXECUTE sp_rename N'dbo.Tmp_ParametroCorreos', N'ParametroCorreos', 'OBJECT' 
GO
ALTER TABLE dbo.ParametroCorreos ADD CONSTRAINT
	PK_ParametroCorreos PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
GO
GO
CREATE TABLE PosicionOffPremiseHeader
	(
	Id int NOT NULL IDENTITY (1, 1),
	Empresa varchar(10) NOT NULL,
	VicePresidencia varchar(200) NOT NULL,
	Departamento varchar(200) NOT NULL,
	Posicion varchar(200) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE PosicionOffPremiseHeader ADD CONSTRAINT
	PK_PosicionOffPremiseHeader PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE PosicionOffPremiseHeader SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
GO
/****** Object:  Table [dbo].[PosicionOffPremiseDetails]    Script Date: 26/01/2022 10:35:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PosicionOffPremiseDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdHeader] [int] NOT NULL,
	[CodigoEmpleado] [int] NOT NULL,
	[Selected] [bit] NOT NULL,
 CONSTRAINT [PK_PosicionOffPremiseDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PosicionOffPremiseDetails] ADD  CONSTRAINT [DF_PosicionOffPremiseDetails_Selected]  DEFAULT ((0)) FOR [Selected]
GO

ALTER TABLE [dbo].[PosicionOffPremiseDetails]  WITH CHECK ADD  CONSTRAINT [FK_PosicionOffPremiseDetails_PosicionOffPremiseHeader] FOREIGN KEY([IdHeader])
REFERENCES [dbo].[PosicionOffPremiseHeader] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[PosicionOffPremiseDetails] CHECK CONSTRAINT [FK_PosicionOffPremiseDetails_PosicionOffPremiseHeader]
GO


