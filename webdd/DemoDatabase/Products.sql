CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](100) NULL,
	[Brand] [nvarchar](100) NULL,
	[Status] [nvarchar](10) NULL,
	[Category] [nvarchar](100) NULL,
	[Price] [int] NULL,
	[ImageUrl] [nvarchar](1000) NULL,
	[CreateAt] [date] NULL,
 CONSTRAINT [PK__Products__3214EC07CE26FCDA] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]	
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_CreateAt]  DEFAULT (dateadd(hour,(8),getutcdate())) FOR [CreateAt]
GO