SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Calendario_festivo](
	[Id] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Descripcion] [nvarchar](100) NOT NULL,
	[Estado] [bit] NULL,
	[FechaActualizacion] [datetime] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Calendario_festivo] ADD PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Calendario_festivo] ADD  DEFAULT (getdate()) FOR [FechaActualizacion]
GO
