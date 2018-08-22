USE [ProjectContentManagerDb]
GO
INSERT [dbo].[Category] ([CategoryId], [Name], [Description], [Root]) VALUES (0, N'Menu', N'Menu', NULL)
INSERT [dbo].[Category] ([CategoryId], [Name], [Description], [Root]) VALUES (1, N'Estados Financieros', N'Estados Financieros', 0)
INSERT [dbo].[Category] ([CategoryId], [Name], [Description], [Root]) VALUES (2, N'Informes de avance', N'Informes de avance', 0)
INSERT [dbo].[ContentType] ([ContentTypeId], [Name], [Description]) VALUES (1, N'Archivo de Word', N'Documento de Word')
INSERT [dbo].[ContentType] ([ContentTypeId], [Name], [Description]) VALUES (2, N'Archivo de Excel', N'Documento de Excel')
INSERT [dbo].[ContentType] ([ContentTypeId], [Name], [Description]) VALUES (3, N'Archivo de PDF', N'Documento PDF')
