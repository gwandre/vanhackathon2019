USE [senseilabs]
GO
/****** Object:  Table [dbo].[tbInnovationData]    Script Date: 3/23/2019 6:59:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbInnovationData](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[businessNumber] [nvarchar](15) NOT NULL,
	[year] [int] NOT NULL,
	[month] [int] NOT NULL,
	[numberOfInnovations] [int] NOT NULL,
	[numberOfProducts] [int] NOT NULL,
 CONSTRAINT [PK_tbInnovationData] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
