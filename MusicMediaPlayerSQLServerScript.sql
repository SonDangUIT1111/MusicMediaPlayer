USE [master]
GO
/****** Object:  Database [MusicPlayer]    Script Date: 3/1/2023 5:20:05 PM ******/
CREATE DATABASE [MusicPlayer]
/****** Object:  Table [dbo].[Album]    Script Date: 3/1/2023 5:20:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Album](
	[AlbumId] [int] IDENTITY(1,1) NOT NULL,
	[AlbumName] [nvarchar](100) NULL,
	[ImageAlbumBinary] [varbinary](max) NULL,
	[UserId] [int] NULL,
	[Composer] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[AlbumId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Artist]    Script Date: 3/1/2023 5:20:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Artist](
	[ArtistId] [int] IDENTITY(1,1) NOT NULL,
	[ArtistName] [nvarchar](100) NULL,
	[ImageArtistBinary] [varbinary](max) NULL,
	[UserId] [int] NULL,
	[Streams] [int] NULL,
	[IsPopular] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ArtistId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genre]    Script Date: 3/1/2023 5:20:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genre](
	[GenreId] [int] IDENTITY(1,1) NOT NULL,
	[GenreName] [nvarchar](100) NULL,
	[ImageGenreBinary] [varbinary](max) NULL,
	[UserId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[GenreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Has]    Script Date: 3/1/2023 5:20:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Has](
	[PlayListId] [int] NOT NULL,
	[SongId] [int] NOT NULL,
 CONSTRAINT [Has_PK] PRIMARY KEY CLUSTERED 
(
	[PlayListId] ASC,
	[SongId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlayList]    Script Date: 3/1/2023 5:20:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlayList](
	[PlayListId] [int] IDENTITY(1,1) NOT NULL,
	[PlayListName] [nvarchar](100) NULL,
	[SongCount] [int] NULL,
	[OwnerId] [int] NOT NULL,
	[ImagePlaylistBinary] [varbinary](max) NULL,
	[TimeCreate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[PlayListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Song]    Script Date: 3/1/2023 5:20:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Song](
	[SongId] [int] IDENTITY(1,1) NOT NULL,
	[SongTitle] [nvarchar](100) NULL,
	[Times] [int] NULL,
	[Artist] [nvarchar](100) NULL,
	[FilePath] [varchar](max) NULL,
	[IsFavourite] [bit] NULL,
	[HowLong] [nvarchar](10) NULL,
	[TimeAdd] [datetime] NULL,
	[ImageSongBinary] [varbinary](max) NULL,
	[UserId] [int] NULL,
	[Genre] [nvarchar](100) NULL,
	[Album] [nvarchar](100) NULL,
	[ArtistId] [int] NULL,
	[AlbumId] [int] NULL,
	[GenreId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[SongId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAccount]    Script Date: 3/1/2023 5:20:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccount](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](100) NULL,
	[UserPassword] [nvarchar](max) NULL,
	[UserEmail] [nvarchar](200) NULL,
	[UserImage] [varbinary](max) NULL,
	[NickName] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Has]  WITH CHECK ADD  CONSTRAINT [Has_FK1] FOREIGN KEY([PlayListId])
REFERENCES [dbo].[PlayList] ([PlayListId])
GO
ALTER TABLE [dbo].[Has] CHECK CONSTRAINT [Has_FK1]
GO
ALTER TABLE [dbo].[Has]  WITH CHECK ADD  CONSTRAINT [Has_FK2] FOREIGN KEY([SongId])
REFERENCES [dbo].[Song] ([SongId])
GO
ALTER TABLE [dbo].[Has] CHECK CONSTRAINT [Has_FK2]
GO
ALTER TABLE [dbo].[PlayList]  WITH CHECK ADD FOREIGN KEY([OwnerId])
REFERENCES [dbo].[UserAccount] ([UserId])
GO
ALTER TABLE [dbo].[Song]  WITH CHECK ADD  CONSTRAINT [FK_Song_Album] FOREIGN KEY([AlbumId])
REFERENCES [dbo].[Album] ([AlbumId])
GO
ALTER TABLE [dbo].[Song] CHECK CONSTRAINT [FK_Song_Album]
GO
ALTER TABLE [dbo].[Song]  WITH CHECK ADD  CONSTRAINT [FK_Song_Artist] FOREIGN KEY([ArtistId])
REFERENCES [dbo].[Artist] ([ArtistId])
GO
ALTER TABLE [dbo].[Song] CHECK CONSTRAINT [FK_Song_Artist]
GO
ALTER TABLE [dbo].[Song]  WITH CHECK ADD  CONSTRAINT [FK_Song_Genre] FOREIGN KEY([GenreId])
REFERENCES [dbo].[Genre] ([GenreId])
GO
ALTER TABLE [dbo].[Song] CHECK CONSTRAINT [FK_Song_Genre]
GO
ALTER TABLE [dbo].[Song]  WITH CHECK ADD  CONSTRAINT [FK_Song_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserAccount] ([UserId])
GO
ALTER TABLE [dbo].[Song] CHECK CONSTRAINT [FK_Song_User]
GO
create trigger trg_Stream
on Song
after UPDATE
as
if (UPDATE(Times))
begin

	declare @artistid int
	select @artistid = i.ArtistId
	from Inserted i
	
	update Artist
	set Streams = Streams + 1
	where ArtistId = @artistid


end
GO
USE [master]
GO
ALTER DATABASE [MusicPlayer] SET  READ_WRITE 
GO
