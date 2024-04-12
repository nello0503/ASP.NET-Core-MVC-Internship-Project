CREATE DATABASE [formazione]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'formazione', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\formazione.mdf' , SIZE = 8192KB , FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'formazione_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\formazione_log.ldf' , SIZE = 8192KB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [formazione] SET COMPATIBILITY_LEVEL = 150
GO
ALTER DATABASE [formazione] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [formazione] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [formazione] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [formazione] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [formazione] SET ARITHABORT OFF 
GO
ALTER DATABASE [formazione] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [formazione] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [formazione] SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF)
GO
ALTER DATABASE [formazione] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [formazione] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [formazione] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [formazione] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [formazione] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [formazione] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [formazione] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [formazione] SET  DISABLE_BROKER 
GO
ALTER DATABASE [formazione] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [formazione] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [formazione] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [formazione] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [formazione] SET  READ_WRITE 
GO
ALTER DATABASE [formazione] SET RECOVERY FULL 
GO
ALTER DATABASE [formazione] SET  MULTI_USER 
GO
ALTER DATABASE [formazione] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [formazione] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [formazione] SET DELAYED_DURABILITY = DISABLED 
GO
USE [formazione]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = Off;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = Primary;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = On;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = Primary;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = Off;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = Primary;
GO
USE [formazione]
GO
IF NOT EXISTS (SELECT name FROM sys.filegroups WHERE is_default=1 AND name = N'PRIMARY') ALTER DATABASE [formazione] MODIFY FILEGROUP [PRIMARY] DEFAULT
GO
USE [Formazione]
GO
/****** Object:  Table [dbo].[Corsi]    Script Date: 13/09/2023 16:09:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Corsi](
	[TipoCorsoID] [int] IDENTITY(1,1) NOT NULL,
	[Codice] [nvarchar](max) NULL,
	[DescrizioneCodice] [nvarchar](max) NULL,
	[Durata] [int] NULL,
	[Sedute] [int] NULL,
	[Moduli] [int] NULL,
	[AnniValidit‡] [int] NULL,
	[MaxDiscenti] [int] NULL,
	[dataorains] [datetime](7) NULL,
	[IDutenteins] [int] NULL,
	[dataoraultmod] [datetime](7) NULL,
	[IDutenteultmod] [int] NULL,
	[eliminato] [smallint] NOT NULL,
	[IDutenteultcanc] [int] NULL,
	[dataoraultcanc] [datetime](7) NULL,
 CONSTRAINT [PK_Corsi] PRIMARY KEY CLUSTERED 
(
	[TipoCorsoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Edizioni]    Script Date: 13/09/2023 16:09:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Edizioni](
	[EdizioneCorsoID] [int] IDENTITY(1,1) NOT NULL,
	[ProgettoFormativoID] [int] NOT NULL,
	[Anno] [int] NULL,
	[NumeroEdizione] [int] NULL,
	[Quantit‡Moduli] [int] NULL,
	[Descrizione] [nvarchar](max) NULL,
	[dataorains] [date] NULL,
	[IDutenteins] [int] NULL,
	[dataoraultmod] [date] NULL,
	[IDutenteultmod] [int] NULL,
	[eliminato] [smallint] NULL,
	[IDutenteultcanc] [int] NULL,
	[dataoraultcanc] [date] NULL,
 CONSTRAINT [PK_Edizioni] PRIMARY KEY CLUSTERED 
(
	[EdizioneCorsoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Moduli]    Script Date: 13/09/2023 16:09:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Moduli](
	[ModuloID] [int] IDENTITY(1,1) NOT NULL,
	[EdizioneCorsoID] [int] NOT NULL,
	[data_inizio] [datetime](7) NOT NULL,
	[totale_ore] [int] NOT NULL,
	[dataorains] [datetime](7) NULL,
	[IDutenteins] [int] NULL,
	[dataoraultmod] [datetime](7) NULL,
	[IDutenteultmod] [int] NULL,
	[eliminato] [smallint] NULL,
	[IDutenteultcanc] [int] NULL,
	[dataoraultcanc] [datetime](7) NULL,
 CONSTRAINT [PK_Moduli] PRIMARY KEY CLUSTERED 
(
	[ModuloID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Progetti]    Script Date: 13/09/2023 16:09:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Progetti](
	[ProgettoFormativoID] [int] IDENTITY(1,1) NOT NULL,
	[TipoCorsoID] [int] NOT NULL,
	[titolo] [nvarchar](max) NULL,
	[direttore_scientifico] [nvarchar](max) NULL,
	[tipologia_evento] [nvarchar](max) NULL,
	[tutor] [nvarchar](max) NULL,
	[tutor_aula] [nvarchar](max) NULL,
	[ore_edizione] [int] NOT NULL,
	[numero_partecipanti] [int] NOT NULL,
	[sede_svolgimento] [nvarchar](max) NULL,
	[dataorains] [datetime](7) NULL,
	[IDutenteins] [int] NULL,
	[dataoraultmod] [datetime](7) NULL,
	[IDutenteultmod] [int] NULL,
	[eliminato] [smallint] NULL,
	[IDutenteultcanc] [int] NULL,
	[dataoraultcanc] [datetime](7) NULL,
 CONSTRAINT [PK_Progetti] PRIMARY KEY CLUSTERED 
(
	[ProgettoFormativoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Edizioni]  WITH CHECK ADD  CONSTRAINT [FK_Edizioni_Progetti_ProgettoFormativoID] FOREIGN KEY([ProgettoFormativoID])
REFERENCES [dbo].[Progetti] ([ProgettoFormativoID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Edizioni] CHECK CONSTRAINT [FK_Edizioni_Progetti_ProgettoFormativoID]
GO
ALTER TABLE [dbo].[Moduli]  WITH CHECK ADD  CONSTRAINT [FK_Moduli_Edizioni_EdizioneCorsoID] FOREIGN KEY([EdizioneCorsoID])
REFERENCES [dbo].[Edizioni] ([EdizioneCorsoID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Moduli] CHECK CONSTRAINT [FK_Moduli_Edizioni_EdizioneCorsoID]
GO
ALTER TABLE [dbo].[Progetti]  WITH CHECK ADD  CONSTRAINT [FK_Progetti_Corsi_TipoCorsoID] FOREIGN KEY([TipoCorsoID])
REFERENCES [dbo].[Corsi] ([TipoCorsoID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Progetti] CHECK CONSTRAINT [FK_Progetti_Corsi_TipoCorsoID]
GO

/****** Object:  Table [dbo].[Discente]    Script Date: 13/09/2023 18:41:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Discente](
	[discente_id] [int] IDENTITY(1,1) NOT NULL,
	[cognome] [varchar](50) NOT NULL,
	[nome] [varchar](50) NOT NULL,
	[luogo_di_nascita] [varchar](50) NULL,
	[data_di_nascita] [date] NULL,
	[cf] [char](16) NULL,
	[unita_operativa] [int] NOT NULL,
	[unita_produttiva] [int] NOT NULL,
	[mansione] [varchar](50) NULL,
	[qualifica] [varchar](50) NULL,
	[dataorains] [datetime] NOT NULL,
	[IDutenteins] [int] NULL,
	[dataoraultmod] [datetime] NULL,
	[IDutenteultmod] [int] NULL,
	[eliminato] [smallint] NOT NULL,
	[IDutenteultcanc] [int] NULL,
	[dataoraultcanc] [datetime] NULL,
 CONSTRAINT [PK_Discente] PRIMARY KEY CLUSTERED 
(
	[discente_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Discente] ADD  CONSTRAINT [DF__Discente__dataor__44FF419A]  DEFAULT (getdate()) FOR [dataorains]
GO

ALTER TABLE [dbo].[Discente] ADD  CONSTRAINT [DF__Discente__elimin__45F365D3]  DEFAULT ((0)) FOR [eliminato]
GO
CREATE TABLE [dbo].[DocenteTutor](
	[DocenteTutorID] [int] IDENTITY(1,1) NOT NULL,
	[qualifica] [varchar](50) NULL,
	[cognome] [varchar](50) NOT NULL,
	[nome] [varchar](50) NOT NULL,
	[tipologia] [smallint] NULL,
	[cf] [char](16) NULL,
	[email] [varchar](50) NULL,
	[telefono] [varchar](50) NULL,
	[cellulare] [varchar](50) NULL,
	[note] [varchar](50) NULL,
	[eliminato] [smallint] NOT NULL,
	[dataorains] [datetime] NOT NULL,
	[IDutenteins] [int] NULL,
	[dataoraultmod] [datetime] NULL,
	[IDutenteultmod] [int] NULL,
	[IDutenteultcanc] [int] NULL,
	[dataoraultcanc] [datetime] NULL,
 CONSTRAINT [PK_DocenteTutor] PRIMARY KEY CLUSTERED 
(
	[DocenteTutorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[DocenteTutor] ADD  CONSTRAINT [DF__DocenteTu__elimi__4222D4EF]  DEFAULT ((0)) FOR [eliminato]
GO

ALTER TABLE [dbo].[DocenteTutor] ADD  CONSTRAINT [DF__DocenteTu__datao__412EB0B6]  DEFAULT (getdate()) FOR [dataorains]
GO
CREATE TABLE [dbo].[Utente](
	[utente_id] [int] IDENTITY(1,1) NOT NULL,
	[cognome] [varchar](50) NULL,
	[nome] [varchar](50) NULL,
	[email] [varchar](50) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password] [varchar](100) NOT NULL,
	[ruolo] [int] NULL,
	[CF] [varchar](16) NULL,
	[unitaprodAbilitate] [nvarchar](200) NULL,
	[eliminato] [smallint] NOT NULL,
	[dataorains] [datetime] NOT NULL,
	[IDutenteins] [int] NULL,
	[dataoraultmod] [datetime] NULL,
	[IDutenteultmod] [int] NULL,
	[IDutenteultcanc] [int] NULL,
	[dataoraultcanc] [datetime] NULL,
 CONSTRAINT [PK_Utente] PRIMARY KEY CLUSTERED 
(
	[utente_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Utente] ADD  CONSTRAINT [DF_Utente_eliminato]  DEFAULT ((0)) FOR [eliminato]
GO

ALTER TABLE [dbo].[Utente] ADD  CONSTRAINT [DF_Utente_dataorains]  DEFAULT (getdate()) FOR [dataorains]
GO
alter table [dbo].[Progetti] add NumeroEdizioni int not null default 0;
go

alter table dbo.moduli add data_fine datetime not null default getdate();
alter table dbo.moduli add numero int not null default 0;
/* Copia Moduli da Edizioni
insert into [dbo].[Moduli]( [EdizioneCorsoID],numero, [data_inizio], [totale_ore], [dataorains], [IDutenteins], [dataoraultmod], [IDutenteultmod], [eliminato], [IDutenteultcanc], [dataoraultcanc], [data_fine])
select    
[EdizioneCorsoID], numero,DATEADD("d",2, [data_inizio]), [totale_ore], [dataorains], [IDutenteins], [dataoraultmod], [IDutenteultmod], 0 [eliminato], [IDutenteultcanc], [dataoraultcanc], 
DATEADD("d",2, [data_fine])
from Moduli
where EdizioneCorsoID=1;
insert into [dbo].[Moduli]( [EdizioneCorsoID],numero, [data_inizio], [totale_ore], [dataorains], [IDutenteins], [dataoraultmod], [IDutenteultmod], [eliminato], [IDutenteultcanc], [dataoraultcanc], [data_fine])
select    
[EdizioneCorsoID]+1, numero,DATEADD("d",7, [data_inizio]), [totale_ore], [dataorains], [IDutenteins], [dataoraultmod], [IDutenteultmod], 0 [eliminato], [IDutenteultcanc], [dataoraultcanc], 
DATEADD("d",7, [data_fine])
from Moduli
where EdizioneCorsoID=3

*/

alter table Progetti add Codice varchar(10) not null default '';




GO

/****** Object:  Table [dbo].[logtable]    Script Date: 27/09/2023 10:00:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[logtable](
	[log_id] [int] IDENTITY(1,1) NOT NULL,
	[dataora] [datetime] NOT NULL,
	[idUtente] [int] NULL,
	[messaggio] [varchar](2048) NULL,
	[tipoOperazione] [varchar](200) NULL
) ON [PRIMARY]
GO


USE [Formazione]
GO


/****** Object:  Table [dbo].[Discente]    Script Date: 20/10/2023 10:32:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Discente](
	[DiscenteID] [int] IDENTITY(1,1) NOT NULL,
	[ProgettoFormativoID] [int] NULL,
	[EdizioneCorsoID] [int] NULL,
	[cognome] [varchar](50) NOT NULL,
	[nome] [varchar](50) NOT NULL,
	[Corso] [varchar](50) NULL,
	[luogo_di_nascita] [varchar](50) NULL,
	[data_di_nascita] [datetime] NULL,
	[cf] [char](16) NULL,
	[unita_operativa] [int] NULL,
	[UnitaProduttiva] [varchar](50) NULL,
	[mansione] [varchar](50) NULL,
	[qualifica] [varchar](50) NULL,
	[eliminato] [int] NOT NULL,
	[UtenteAss] [varchar](50) NULL,
	[dataorains] [datetime] NOT NULL,
	[IDutenteins] [int] NULL,
	[dataoraultmod] [datetime] NULL,
	[IDutenteultmod] [int] NULL,
	[IDutenteultcanc] [int] NULL,
	[dataoraultcanc] [datetime] NULL,
	[Assegnato] [bit] NULL,
	[DataAssegnato] [datetime] NULL,
 CONSTRAINT [PK_Discente] PRIMARY KEY CLUSTERED 
(
	[DiscenteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Discente] ADD  CONSTRAINT [DF_Discente_eliminato]  DEFAULT ((0)) FOR [eliminato]
GO

ALTER TABLE [dbo].[Discente] ADD  CONSTRAINT [DF_Discente_dataorains]  DEFAULT (getdate()) FOR [dataorains]
GO

