USE [insitedb]
GO
/****** Object:  Table [dbo].[ParametroCorreos]    Script Date: 03/11/2021 10:28:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ParametroCorreos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Destinatario] [varchar](250) NULL,
	[Empresa] [nvarchar](10) NULL,
	[Vicepresidencia] [nvarchar](200) NULL,
	[Departamento] [nvarchar](200) NULL,
	[Indicadores] [nvarchar](1000) NULL,
 CONSTRAINT [PK_ParametroCorreos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ParametroCorreos] ON 
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (332, NULL, N'1002', N'Administración', N'Cadena de Suministros y Administración', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (333, NULL, N'1002', N'Administración', N'Taller de Mecánica', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (334, NULL, N'1002', N'Administración', N'Transportación', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (335, NULL, N'1002', N'Administración', N'Zona 3', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (336, NULL, N'1002', N'Administración', N'Zona 4', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (337, NULL, N'1002', N'Gerencia General', N'Cadena de Suministros y Administración', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (338, NULL, N'1002', N'Gerencia General', N'Gerencia General', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (339, NULL, N'1002', N'Gerencia General', N'Mantenimiento Industrial', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (340, NULL, N'1002', N'Gerencia General', N'Obras Civiles e Infraestructura', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (341, NULL, N'1002', N'Gerencia General', N'Planta de Beneficio Integral (PBI)', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (342, NULL, N'1002', N'Gerencia General', N'Sanidad Vegetal y Control de Calidad', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (343, NULL, N'1002', N'Gerencia General', N'Semovientes y Vivero', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (344, NULL, N'1002', N'Gerencia General', N'Taller de Mecánica', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (345, NULL, N'1002', N'Gerencia General', N'Transportación', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (346, NULL, N'1002', N'Gerencia General', N'Zona 1', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (347, NULL, N'1002', N'Gerencia General', N'Zona 2', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (348, NULL, N'1002', N'Gerencia General', N'Zona 3', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (349, NULL, N'1002', N'Gerencia General', N'Zona 4', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (350, NULL, N'1002', N'Presidencia Ejecutiva GrupoSID', N'Dir. Corp. de Recursos Humanos', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (351, NULL, N'1002', N'Presidencia Ejecutiva GrupoSID', N'Presidencia Ejecutiva GrupoSID', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (352, NULL, N'1002', N'V.P. Corp. de Finanzas', N'Gerencia Corp. de Seguridad Física', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (353, NULL, N'1002', N'V.P. Corp. de Operaciones', N'Dir. Corp. Calidad, Seg. y Medioambiente', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (354, NULL, N'1002', N'V.P. Corp. de Operaciones', N'Mantenimiento Industrial', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (355, NULL, N'1002', N'V.P. Corp. de Operaciones', N'Planta de Beneficio Integral (PBI)', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (356, NULL, N'1002', N'V.P. Corp. de Operaciones', N'Salud, seguridad y ambien', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (357, NULL, N'1002', N'V.P. Corp. de Operaciones', N'Taller de Mecánica', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (358, NULL, N'1002', N'V.P. Corp. de Operaciones', N'Transportación', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (359, NULL, N'1002', N'Vicepresidencia Corp. de Compr', N'Vicepresidencia Corp. de Compras', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (360, NULL, N'1004', N'Otras', N'Otras', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (361, NULL, N'1004', N'Otras', N'Servicios Externos', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (362, NULL, N'1004', N'Presidencia Ejecutiva GrupoSID', N'Club Empleados', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (363, NULL, N'1004', N'Presidencia Ejecutiva GrupoSID', N'Dir. Corp. de Comunicación', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (364, NULL, N'1004', N'Presidencia Ejecutiva GrupoSID', N'Dir. Corp. de Recursos Humanos', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (365, NULL, N'1004', N'Presidencia Ejecutiva GrupoSID', N'Presidencia Ejecutiva', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (366, NULL, N'1004', N'V.P. Corp. de Compras', N'Gerencia Corp. de Compras', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (367, NULL, N'1004', N'V.P. Corp. de Compras', N'Vicepresidencia Corp. de Compras', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (368, NULL, N'1004', N'V.P. Corp. de Finanzas', N'Dir. Corp. de Tecnología y Transf. Digit', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (369, NULL, N'1004', N'V.P. Corp. de Finanzas', N'Dirección Corp. de Auditoría y Riesgos', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (370, NULL, N'1004', N'V.P. Corp. de Finanzas', N'Gcia. Nuevos Proyectos Man. y Mat.', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (371, NULL, N'1004', N'V.P. Corp. de Finanzas', N'Gerencia Corp. Administración de Ventas', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (372, NULL, N'1004', N'V.P. Corp. de Finanzas', N'Gerencia Corp. de Aplicaciones', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (373, NULL, N'1004', N'V.P. Corp. de Finanzas', N'Gerencia Corp. de Costos', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (374, NULL, N'1004', N'V.P. Corp. de Finanzas', N'Gerencia Corp. de Impuestos', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (375, NULL, N'1004', N'V.P. Corp. de Finanzas', N'Gerencia Corp. de Riesgos', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (376, NULL, N'1004', N'V.P. Corp. de Finanzas', N'Gerencia Corp. de Seguridad Física', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (377, NULL, N'1004', N'V.P. Corp. de Finanzas', N'Gerencia Corp. de Servicios TI', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (378, NULL, N'1004', N'V.P. Corp. de Finanzas', N'Gerencia Corp. de Tesorería', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (379, NULL, N'1004', N'V.P. Corp. de Finanzas', N'Gerencia Corp. Infraest. Tecnológica', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (380, NULL, N'1004', N'V.P. Corp. de Finanzas', N'Gerencia Corp. Tráfico y Aduanas', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (381, NULL, N'1004', N'V.P. Corp. de Finanzas', N'Gerencia de Análisis Financiero', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (382, NULL, N'1004', N'V.P. Corp. de Finanzas', N'Gerencia de Auditoría', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (383, NULL, N'1004', N'V.P. Corp. de Finanzas', N'Gerencia de Contabilidad Financiera', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (384, NULL, N'1004', N'V.P. Corp. de Finanzas', N'Vicepresidencia Corp. de Finanzas', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (385, NULL, N'1004', N'V.P. Corp. de Finanzas', N'Vicepresidencia M & A', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (386, NULL, N'1004', N'V.P. Corp. de Operaciones', N'Dir. Corp. Calidad, Seg. y Medioambiente', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (387, NULL, N'1004', N'V.P. Corp. de Operaciones', N'Gerencia Centros de Distribución', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (388, NULL, N'1004', N'V.P. Corp. de Operaciones', N'Gerencia Corp. de Promoción y Eventos', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (389, NULL, N'1004', N'V.P. Corp. de Operaciones', N'Gerencia Corp. de Seguridad Industrial', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (390, NULL, N'1004', N'V.P. Corp. de Operaciones', N'Gerencia de Aceites', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (391, NULL, N'1004', N'V.P. Corp. de Operaciones', N'Gerencia de Calidad', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (392, NULL, N'1004', N'V.P. Corp. de Operaciones', N'Gerencia de Desarrollo', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (393, NULL, N'1004', N'V.P. Corp. de Operaciones', N'Gerencia de Envases', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (394, NULL, N'1004', N'V.P. Corp. de Operaciones', N'Gerencia de Ingeniería y Proyectos', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (395, NULL, N'1004', N'V.P. Corp. de Operaciones', N'Gerencia de Innovación', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (396, NULL, N'1004', N'V.P. Corp. de Operaciones', N'Gerencia de Medio Ambiente', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (397, NULL, N'1004', N'V.P. Corp. de Operaciones', N'Gerencia de Op. Log. y Planificación', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (398, NULL, N'1004', N'V.P. Corp. de Operaciones', N'Gerencia de Planificación', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (399, NULL, N'1004', N'V.P. Corp. de Operaciones', N'Gerencia de Planificación y Materiales', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (400, NULL, N'1004', N'V.P. Corp. de Operaciones', N'Gerencia de Transportación', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (401, NULL, N'1004', N'V.P. Corp. de Operaciones', N'Gerencia Mantenimiento Industrial', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (402, NULL, N'1004', N'V.P. Corp. de Operaciones', N'Gerencia Manufactura de Alimentos', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (403, NULL, N'1004', N'V.P. Corp. de Operaciones', N'Gerencia Manufactura Lácteos', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (404, NULL, N'1004', N'V.P. Corp. de Operaciones', N'Gerencia Planta Maicera', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (405, NULL, N'1004', N'V.P. Corp. de Operaciones', N'Gerencia Producción de Detergentes', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (406, NULL, N'1004', N'V.P. Corp. de Operaciones', N'Mantenimiento Industrial', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (407, NULL, N'1004', N'V.P. Corp. de Operaciones', N'SIDport', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (408, NULL, N'1004', N'V.P. Corp. de Operaciones', N'Vicepresidencia Corp. de Operaciones', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (409, NULL, N'1004', N'V.P. Corp. Neg. Int./Export.', N'Vicepresidencia Corp. Neg. Int./Export.', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (410, NULL, N'1004', N'V.P. Ejecutiva', N'Dir. Corp. de Recursos Humanos', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (411, NULL, N'1004', N'V.P. Ejecutiva', N'Gerencia Corp. de Promoción y Eventos', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (412, NULL, N'1004', N'V.P. Ejecutiva', N'Gerencia de Negocios Aceites y Grasas', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (413, NULL, N'1004', N'V.P. Ejecutiva', N'Gerencia de Negocios Bebidas', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (414, NULL, N'1004', N'V.P. Ejecutiva', N'Gerencia de Ventas BARECA', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (415, NULL, N'1004', N'V.P. Ejecutiva', N'Gerencia de Ventas CEB', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (416, NULL, N'1004', N'V.P. Ejecutiva', N'Gerencia de Ventas Clientes Potenciales', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (417, NULL, N'1004', N'V.P. Ejecutiva', N'Gerencia de Ventas Directas', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (418, NULL, N'1004', N'V.P. Ejecutiva', N'Gerencia de Ventas Foodservice', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (419, NULL, N'1004', N'V.P. Ejecutiva', N'Gerencia de Ventas KAN', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (420, NULL, N'1004', N'V.P. Ejecutiva', N'Gerencia de Ventas KC', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (421, NULL, N'1004', N'V.P. Ejecutiva', N'Gerencia de Ventas Mayoristas', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (422, NULL, N'1004', N'V.P. Ejecutiva', N'Gerencia de Ventas SMI', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (423, NULL, N'1004', N'V.P. Ejecutiva', N'Gerencia General de Ventas', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (424, NULL, N'1004', N'V.P. Ejecutiva', N'Gerencia Neg. Foodservice e Industrias', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (425, NULL, N'1004', N'V.P. Ejecutiva', N'Gerencia Neg. Productos Pers. y Limpieza', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (426, NULL, N'1004', N'V.P. Ejecutiva', N'Gerencia Servicio al Clie', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (427, NULL, N'1004', N'V.P. Ejecutiva', N'Haagen Dazs', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (428, NULL, N'1004', N'V.P. Ejecutiva', N'Vicepresidencia Ejecutiva', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (429, NULL, N'1004', N'V.P. Neg Internacionales y Exp', N'Vicepresidencia Corp. Neg. Int./Export.', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (430, NULL, N'1004', N'V.P. Neg Internacionales y Exp', N'Vicepresidencia Neg. Int. y Exportación', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (431, NULL, N'1004', N'VP Administración (D)', N'Gerencia Corp. de Seguridad Física', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (432, NULL, N'1004', N'VP Finanzas (D)', N'Gerencia Corp. Infraest. Tecnológica', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (433, NULL, N'1007', N'Gerencia General', N'Gerencia de Ventas', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (434, NULL, N'1007', N'Presidencia Ejecutiva GrupoSID', N'Dir. Corp. de Recursos Humanos', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (435, NULL, N'1007', N'V.P. Corp. de Finanzas', N'Gerencia Corp. Administración de Ventas', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (436, NULL, N'1007', N'V.P. Corp. de Finanzas', N'Gerencia Corp. de Riesgos', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (437, NULL, N'1007', N'V.P. Corp. de Finanzas', N'Gerencia Corp. de Seguridad Física', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (438, NULL, N'1007', N'V.P. Corp. de Operaciones', N'Dir. Corp. Calidad, Seg. y Medioambiente', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (439, NULL, N'1007', N'V.P. Corp. de Operaciones', N'Gerencia Corp. Seguridad Industrial', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (440, NULL, N'1007', N'V.P. Corp. de Operaciones', N'Gerencia de Producción', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (441, NULL, N'1007', N'V.P. Corp. de Operaciones', N'Gerencia de Ventas', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (442, NULL, N'1007', N'V.P. Corp. de Operaciones', N'Gerencia Mantenimiento de Flotilla', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (443, NULL, N'1007', N'V.P. Corp. de Operaciones', N'Gerencia Mantenimiento Industrial', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (444, NULL, N'1007', N'V.P. Ejecutiva', N'Gerencia Agua Crystal', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (445, NULL, N'1007', N'V.P. Ejecutiva', N'Gerencia de Ventas', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (446, NULL, N'1016', N'Gerencia General', N'Gerencia General', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (447, NULL, N'1016', N'Gerencia General', N'Transportación', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (448, NULL, N'1016', N'V.P. Corp. de Finanzas', N'Gerencia Corp. de Seguridad Física', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (449, NULL, N'INDV', N'Presidencia Ejecutiva GrupoSID', N'Dir. Corp. de Recursos Humanos', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (450, NULL, N'INDV', N'Presidencia Ejecutiva GrupoSID', N'Presidencia Ejecutiva', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (451, NULL, N'INDV', N'V.P. Corp. de Compras', N'Gerencia Compras Insumos', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (452, NULL, N'INDV', N'V.P. Corp. de Compras', N'Gerencia Corp. de Compras', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (453, NULL, N'INDV', N'V.P. Corp. de Compras', N'Vicepresidencia Corp. de Compras', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (454, NULL, N'INDV', N'V.P. Corp. de Finanzas', N'Dir. Corp. de Tecnología y Transf. Digit', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (455, NULL, N'INDV', N'V.P. Corp. de Finanzas', N'Gerencia Corp. Administración de Ventas', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (456, NULL, N'INDV', N'V.P. Corp. de Finanzas', N'Gerencia Corp. de Aplicaciones', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (457, NULL, N'INDV', N'V.P. Corp. de Finanzas', N'Gerencia Corp. de Costos', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (458, NULL, N'INDV', N'V.P. Corp. de Finanzas', N'Gerencia Corp. de Legal', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (459, NULL, N'INDV', N'V.P. Corp. de Finanzas', N'Gerencia Corp. de Riesgos', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (460, NULL, N'INDV', N'V.P. Corp. de Finanzas', N'Gerencia Corp. de Seguridad Física', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (461, NULL, N'INDV', N'V.P. Corp. de Finanzas', N'Gerencia Corp. de Servicios TI', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (462, NULL, N'INDV', N'V.P. Corp. de Finanzas', N'Gerencia Corp. de Tesorería', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (463, NULL, N'INDV', N'V.P. Corp. de Finanzas', N'Gerencia de Auditoría', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (464, NULL, N'INDV', N'V.P. Corp. de Finanzas', N'Gerencia de Contabilidad Financiera', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (465, NULL, N'INDV', N'V.P. Corp. de Finanzas', N'Vicepresidencia Corp. de Tesorería', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (466, NULL, N'INDV', N'V.P. Corp. de Operaciones', N'Dir. Corp. Calidad, Seg. y Medioambiente', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (467, NULL, N'INDV', N'V.P. Corp. de Operaciones', N'Gcia. Corp. Aseguramiento Calidad Bebida', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (468, NULL, N'INDV', N'V.P. Corp. de Operaciones', N'Gerencia Centros de Distribución', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (469, NULL, N'INDV', N'V.P. Corp. de Operaciones', N'Gerencia Corp. de Seguridad Industrial', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (470, NULL, N'INDV', N'V.P. Corp. de Operaciones', N'Gerencia Corp. Mant. Flot. Centros Dist.', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (471, NULL, N'INDV', N'V.P. Corp. de Operaciones', N'Gerencia de Calidad', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (472, NULL, N'INDV', N'V.P. Corp. de Operaciones', N'Gerencia de Innovación Lácteos', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (473, NULL, N'INDV', N'V.P. Corp. de Operaciones', N'Gerencia de Innovación y Desarrollo', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (474, NULL, N'INDV', N'V.P. Corp. de Operaciones', N'Gerencia de Mant. Industrial Bebidas', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (475, NULL, N'INDV', N'V.P. Corp. de Operaciones', N'Gerencia de Manufactura Cárnica', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (476, NULL, N'INDV', N'V.P. Corp. de Operaciones', N'Gerencia de Manufactura Lácteos y Bebida', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (477, NULL, N'INDV', N'V.P. Corp. de Operaciones', N'Gerencia de Medio Ambiente', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (478, NULL, N'INDV', N'V.P. Corp. de Operaciones', N'Gerencia de Op. Log. y Planificación', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (479, NULL, N'INDV', N'V.P. Corp. de Operaciones', N'Gerencia de Planificación y Materiales', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (480, NULL, N'INDV', N'V.P. Corp. de Operaciones', N'Gerencia de Transportación', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (481, NULL, N'INDV', N'V.P. Corp. de Operaciones', N'Gerencia Mantenimiento Industrial', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (482, NULL, N'INDV', N'V.P. Corp. de Operaciones', N'Gerencia Producción de Yogurt y Quesos', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (483, NULL, N'INDV', N'V.P. Corp. de Operaciones', N'Gerencia Proyectos de Ingeniería', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (484, NULL, N'INDV', N'V.P. Corp. de Operaciones', N'Vicepresidencia Corp. de Operaciones', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (485, NULL, N'INDV', N'V.P. Corp. Neg. Int./Export.', N'Vicepresidencia Corp. Neg. Int./Export.', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (486, NULL, N'INDV', N'V.P. Ejecutiva', N'Gerencia Corp. de Promoción y Eventos', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (487, NULL, N'INDV', N'V.P. Ejecutiva', N'Gerencia de Ventas Detallista', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (488, NULL, N'INDV', N'V.P. Ejecutiva', N'Gerencia de Ventas Distrib. y Fronteras', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (489, NULL, N'INDV', N'V.P. Ejecutiva', N'Gerencia de Ventas Foodservice', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (490, NULL, N'INDV', N'V.P. Ejecutiva', N'Gerencia de Ventas KAN', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (491, NULL, N'INDV', N'V.P. Ejecutiva', N'Gerencia de Ventas SMI', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (492, NULL, N'INDV', N'V.P. Ejecutiva', N'Gerencia General de Ventas', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (493, NULL, N'INDV', N'V.P. Ejecutiva', N'Gerencia Servicio al Cliente', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (494, NULL, N'INDV', N'V.P. Ejecutiva', N'Vicepresidencia Ejecutiva', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (495, NULL, N'INDV', N'V.P. Neg Internacionales y Exp', N'Vicepresidencia Corp. Neg. Int./Export.', N'DataInasistencias,DataSalidaFueraHorario')
GO
INSERT [dbo].[ParametroCorreos] ([Id], [Destinatario], [Empresa], [Vicepresidencia], [Departamento], [Indicadores]) VALUES (496, NULL, N'INDV', N'V.P. Neg Internacionales y Exp', N'Vicepresidencia Neg. Int. y Exportación', N'DataInasistencias,DataSalidaFueraHorario')
GO
SET IDENTITY_INSERT [dbo].[ParametroCorreos] OFF
GO

/**/
insert into Parametros 
([Parametro], [Valor])
values
('SendEmailInasistenciaIncumplimientoJob','0 0 21 * * 1,2,3,4,5')