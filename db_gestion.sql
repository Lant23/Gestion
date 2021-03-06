USE [db_gestion]
GO
/****** Object:  Table [dbo].[Utilisateur]    Script Date: 01/02/2016 10:00:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Utilisateur](
	[id_utilisateur] [int] IDENTITY(1,1) NOT NULL,
	[nom] [nvarchar](50) NOT NULL,
	[prenom] [nvarchar](50) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[mot_de_passe] [nvarchar](50) NOT NULL,
	[telephone] [nvarchar](50) NOT NULL,
	[adresse] [nvarchar](50) NOT NULL,
	[code_postal] [nchar](10) NOT NULL,
	[ville] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__Utilisat__1A4FA5B8BD986253] PRIMARY KEY CLUSTERED 
(
	[id_utilisateur] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
