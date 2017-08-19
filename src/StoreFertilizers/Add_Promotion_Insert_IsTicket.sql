USE [StoreFertilizersDB]
GO

ALTER TABLE dbo.Invoice
ADD IsTicket bit NULL
GO

ALTER TABLE dbo.InvoiceDetails
ADD IsTicket bit NULL
GO

/****** Object:  Table [dbo].[Promotion]    Script Date: 17/8/2560 21:12:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Promotion](
	[PromotionID] [int] IDENTITY(1,1) NOT NULL,
	[Descr] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Promotion] PRIMARY KEY CLUSTERED 
(
	[PromotionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


