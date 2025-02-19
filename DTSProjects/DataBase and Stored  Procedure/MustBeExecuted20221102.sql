Use Nufarm;
GO


IF NOT EXISTS(SELECT ColumnName FROM (
   SELECT c.[Object_ID],t.[name],c.[name] as ColumnName from Sys.tables t INNER JOIN Sys.columns c
   ON c.[object_id] = t.[object_id]
   WHERE t.[name] = 'GON_DETAIL')C WHERE ColumnName = 'UNIT1')
   ALTER TABLE GON_DETAIL ADD UNIT1 VARCHAR(30) NULL;
GO
IF NOT EXISTS(SELECT ColumnName FROM (
   SELECT c.[Object_ID],t.[name],c.[name] as ColumnName from Sys.tables t INNER JOIN Sys.columns c
   ON c.[object_id] = t.[object_id]
   WHERE t.[name] = 'GON_DETAIL')C WHERE ColumnName = 'VOL1')
   ALTER TABLE GON_DETAIL ADD VOL1 DECIMAL(18,3) NULL;
GO
IF NOT EXISTS(SELECT ColumnName FROM (
   SELECT c.[Object_ID],t.[name],c.[name] as ColumnName from Sys.tables t INNER JOIN Sys.columns c
   ON c.[object_id] = t.[object_id]
   WHERE t.[name] = 'GON_DETAIL')C WHERE ColumnName = 'UNIT2')
   ALTER TABLE GON_DETAIL ADD UNIT2 VARCHAR(30) NULL;
GO
IF NOT EXISTS(SELECT ColumnName FROM (
   SELECT c.[Object_ID],t.[name],c.[name] as ColumnName from Sys.tables t INNER JOIN Sys.columns c
   ON c.[object_id] = t.[object_id]
   WHERE t.[name] = 'GON_DETAIL')C WHERE ColumnName = 'VOL2')
   ALTER TABLE GON_DETAIL ADD VOL2 DECIMAL(18,3) NULL;
GO
--tambahkan column BatchNo untuk GON_DETAIL
IF NOT EXISTS(SELECT ColumnName FROM (
   SELECT c.[Object_ID],t.[name],c.[name] as ColumnName from Sys.tables t INNER JOIN Sys.columns c
   ON c.[object_id] = t.[object_id]
   WHERE t.[name] = 'GON_DETAIL')C WHERE ColumnName = 'BatchNO')
   ALTER TABLE GON_DETAIL ADD BatchNo VARCHAR(50) NULL;
GO


--Tambahkan column WARHOUSE di GON_DETAIL
IF NOT EXISTS(SELECT ColumnName FROM (
   SELECT c.[Object_ID],t.[name],c.[name] as ColumnName from Sys.tables t INNER JOIN Sys.columns c
   ON c.[object_id] = t.[object_id]
   WHERE t.[name] = 'GON_HEADER')C WHERE ColumnName = 'WARHOUSE')
   ALTER TABLE GON_HEADER ADD WARHOUSE VARCHAR(20) NULL;
GO
IF NOT EXISTS(SELECT ColumnName FROM (
   SELECT c.[Object_ID],t.[name],c.[name] as ColumnName from Sys.tables t INNER JOIN Sys.columns c
   ON c.[object_id] = t.[object_id]
   WHERE t.[name] = 'GON_HEADER')C WHERE ColumnName = 'POLICE_NO_TRANS')
   ALTER TABLE GON_HEADER ADD POLICE_NO_TRANS VARCHAR(50) NULL;
GO
IF NOT EXISTS(SELECT ColumnName FROM (
   SELECT c.[Object_ID],t.[name],c.[name] as ColumnName from Sys.tables t INNER JOIN Sys.columns c
   ON c.[object_id] = t.[object_id]
   WHERE t.[name] = 'GON_HEADER')C WHERE ColumnName = 'DRIVER_TRANS')
   ALTER TABLE GON_HEADER ADD DRIVER_TRANS VARCHAR(50) NULL;
GO

--isi deskripsi warhousenya
--data warhouse di isi dengan singkatan saja
--untuk sekarang di kategorikan 5 gudang saja
EXEC sys.sp_addextendedproperty @name = N'MS_Description', 
@value = N'Kode Gudang SRG = SERANG, SBY = SURABAYA, JKT = JAKARTA,MRK = MERAK,TGR = TANGERANG', 
@level0type = N'SCHEMA',
@level0name = N'dbo',
@level1type = N'TABLE',
@level1name = N'GON_HEADER',
@level2type = N'COLUMN',
@level2name = N'WARHOUSE'
GO



--UPDATE BRND_PROD_CONV SET INACTIVE = 0;
IF NOT EXISTS(SELECT FORM_NAME FROM SYST_MENU WHERE FORM_NAME = 'GONWithoutPOMaster')
BEGIN
INSERT  INTO SYST_MENU(FORM_ID,FORM_NAME,MENU_NAME,DESCRIPTIONS)
VALUES('GUF001','GONWithoutPOMaster','Non_Sales_GON','SEPARATED GON FOR NON PO DISTRIBUTOR')
END
GO
IF NOT EXISTS(SELECT FORM_NAME FROM SYST_MENU WHERE FORM_NAME = 'GONWithoutPOMaster')
BEGIN
INSERT  INTO SYST_MENU(FORM_ID,FORM_NAME,MENU_NAME,DESCRIPTIONS)
VALUES('BRND7','ConvertionProduct','Convertion','Data Konversi Product')
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BRND_PROD_CONV]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[BRND_PROD_CONV](
	[IDApp] [int] IDENTITY(1,1) NOT NULL,
	[BRANDPACK_ID] [varchar](14) NOT NULL,
	[UNIT1] [varchar](30) NOT NULL,
	[VOL1] [decimal](18, 3) NOT NULL,
	[UNIT2] [varchar](30) NULL,
	[VOL2] [decimal](18, 3) NULL,
	[INACTIVE] [bit] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedDate] [smalldatetime] NOT NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [smalldatetime] NULL,
 CONSTRAINT [PK_BRND_PROD_CONV] PRIMARY KEY CLUSTERED 
(
	[IDApp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GON_SEPARATED_DETAIL]    Script Date: 17/04/2023 11:44:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GON_SEPARATED_DETAIL]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GON_SEPARATED_DETAIL](
	[IDApp] [int] IDENTITY(1,1) NOT NULL,
	[FKAppGonHeader] [int] NOT NULL,
	[FKAppPODetail] [int] NOT NULL,
	[ITEM] [varchar](14) NOT NULL,
	[QTY] [decimal](18, 4) NOT NULL CONSTRAINT [DF_GON_SEPARATED_DETAIL_QTY]  DEFAULT ((0)),
	[COLLY_BOX] [varchar](50) NULL,
	[COLLY_PACKSIZE] [varchar](50) NULL,
	[BATCH_NO] [numeric](18, 0) NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedDate] [smalldatetime] NOT NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [smalldatetime] NULL,
 CONSTRAINT [PK_GON_SEPARATED_DETAIL] PRIMARY KEY CLUSTERED 
(
	[IDApp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GON_SEPARATED_HEADER]    Script Date: 17/04/2023 11:44:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GON_SEPARATED_HEADER]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GON_SEPARATED_HEADER](
	[IDApp] [int] IDENTITY(1,1) NOT NULL,
	[FKApp] [int] NOT NULL,
	[GON_NUMBER] [varchar](50) NOT NULL,
	[GON_DATE] [smalldatetime] NOT NULL,
	[WARHOUSE] [varchar](20) NOT NULL,
	[SHIP_TO] [varchar](10) NULL,
	[SHIP_TO_CUST] [varchar](50) NOT NULL CONSTRAINT [DF_GON_SEPARATED_HEADER_SHIP_TO_CUST]  DEFAULT (''),
	[SHIP_TO_ADDRESS] [varchar](150) NOT NULL CONSTRAINT [DF_GON_SEPARATED_HEADER_SHIP_TO_ADDRESS]  DEFAULT (''),
	[TRANSPORTER] [varchar](16) NULL,
	[POLICE_NO_TRANS] [varchar](50) NOT NULL CONSTRAINT [DF_GON_SEPARATED_HEADER_POLICE_NO_TRANS]  DEFAULT (''),
	[DRIVER_TRANS] [varchar](50) NOT NULL CONSTRAINT [DF_GON_SEPARATED_HEADER_DRIVER_TRANS]  DEFAULT (''),
	[GON_AREA] [varchar](20) NULL,
	[REMARK] [varchar](250) NULL,
	[CreatedDate] [smalldatetime] NOT NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedDate] [smalldatetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
 CONSTRAINT [PK_GON_SEPARATED_HEADER] PRIMARY KEY CLUSTERED 
(
	[IDApp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GON_SEPARATED_PO_DETAIL]    Script Date: 17/04/2023 11:44:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GON_SEPARATED_PO_DETAIL]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GON_SEPARATED_PO_DETAIL](
	[IDApp] [int] IDENTITY(1,1) NOT NULL,
	[FKApp] [int] NOT NULL,
	[BRANDPACK_ID] [varchar](14) NOT NULL,
	[QUANTITY] [decimal](18, 3) NOT NULL,
	[STATUS] [varchar](30) NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedDate] [smalldatetime] NOT NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [smalldatetime] NULL,
 CONSTRAINT [PK_GON_SEPARATED_PO_DETAIL] PRIMARY KEY CLUSTERED 
(
	[IDApp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GON_SEPARATED_PO_HEADER]    Script Date: 17/04/2023 11:44:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GON_SEPARATED_PO_HEADER]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[GON_SEPARATED_PO_HEADER](
	[IDApp] [int] IDENTITY(1,1) NOT NULL,
	[PO_NUMBER] [varchar](50) NULL,
	[PO_DATE] [smalldatetime] NULL,
	[SPPB_NUMBER] [varchar](50) NULL,
	[SPPB_DATE] [smalldatetime] NULL,
	[CreatedDate] [smalldatetime] NULL,
	[CreatedBy] [varchar](50) NULL,
	[ModifiedDate] [smalldatetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
 CONSTRAINT [PK_GON_SEPARATED_PO_HEADER] PRIMARY KEY CLUSTERED 
(
	[IDApp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[BRND_PROD_CONV] ON 

GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, N'11014100MD', N'LTR', CAST(5.000 AS Decimal(18, 3)), N'BOTOL', CAST(50.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, N'11014250MD', N'LTR', CAST(10.000 AS Decimal(18, 3)), N'BOTOL', CAST(40.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (3, N'11014500MD', N'LTR', CAST(10.000 AS Decimal(18, 3)), N'BOTOL', CAST(20.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (4, N'11014001LD', N'LTR', CAST(15.000 AS Decimal(18, 3)), N'BOTOL', CAST(15.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (5, N'11043080MD', N'LTR', CAST(4.000 AS Decimal(18, 3)), N'BOTOL', CAST(50.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (6, N'11043250MD', N'LTR', CAST(5.000 AS Decimal(18, 3)), N'BOTOL', CAST(20.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (7, N'11059100MD', N'LTR', CAST(5.000 AS Decimal(18, 3)), N'BOTOL', CAST(50.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (8, N'11059250MD', N'LTR', CAST(5.000 AS Decimal(18, 3)), N'BOTOL', CAST(20.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (9, N'00905500MD', N'LTR', CAST(10.000 AS Decimal(18, 3)), N'BOTOL', CAST(20.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (10, N'00905001LD', N'LTR', CAST(10.000 AS Decimal(18, 3)), N'BOTOL', CAST(10.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (11, N'00081500MD', N'LTR', CAST(10.000 AS Decimal(18, 3)), N'BOTOL', CAST(20.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (12, N'00081001LD', N'LTR', CAST(10.000 AS Decimal(18, 3)), N'BOTOL', CAST(10.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (13, N'11066005LE', N'LTR', CAST(20.000 AS Decimal(18, 3)), N'BOTOL', CAST(4.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (14, N'11066020LE', N'LTR', CAST(20.000 AS Decimal(18, 3)), N'JERIGEN', CAST(1.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (15, N'00325025KD', N'KG', CAST(25.000 AS Decimal(18, 3)), N'SAK', CAST(1.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (16, N'00780001LD', N'LTR', CAST(12.000 AS Decimal(18, 3)), N'BOTOL', CAST(12.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (17, N'00780004LD', N'LTR', CAST(16.000 AS Decimal(18, 3)), N'BOTOL', CAST(4.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (18, N'07280080MD', N'LTR', CAST(4.000 AS Decimal(18, 3)), N'BOTOL', CAST(50.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (19, N'07280250MD', N'LTR', CAST(10.000 AS Decimal(18, 3)), N'BOTOL', CAST(40.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (20, N'011009100M-D', N'LTR', CAST(5.000 AS Decimal(18, 3)), N'BOTOL', CAST(50.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (21, N'011009200MD', N'LTR', CAST(8.000 AS Decimal(18, 3)), N'BOTOL', CAST(40.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (22, N'011009500M-D', N'LTR', CAST(10.000 AS Decimal(18, 3)), N'BOTOL', CAST(20.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (23, N'77004001KD', N'KG', CAST(10.000 AS Decimal(18, 3)), N'POUCH', CAST(10.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (24, N'11021001KD', N'KG', CAST(10.000 AS Decimal(18, 3)), N'POUCH', CAST(10.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (25, N'11006100MD', N'LTR', CAST(5.000 AS Decimal(18, 3)), N'BOTOL', CAST(50.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (26, N'11006250MD', N'LTR', CAST(5.000 AS Decimal(18, 3)), N'BOTOL', CAST(20.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (27, N'11006500MD', N'LTR', CAST(10.000 AS Decimal(18, 3)), N'BOTOL', CAST(20.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (28, N'0008450CPD', N'LTR', CAST(1.000 AS Decimal(18, 3)), N'INNER', CAST(20.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (29, N'0091425CPD', N'KG', CAST(2.000 AS Decimal(18, 3)), N'SACHET', CAST(80.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (30, N'00910020LD', N'LTR', CAST(20.000 AS Decimal(18, 3)), N'JERIGEN', CAST(1.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (31, N'11015250MD', N'LTR', CAST(10.000 AS Decimal(18, 3)), N'BOTOL', CAST(40.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (32, N'11015500MD', N'LTR', CAST(10.000 AS Decimal(18, 3)), N'BOTOL', CAST(20.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (33, N'11015001LD', N'LTR', CAST(15.000 AS Decimal(18, 3)), N'BOTOL', CAST(15.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (34, N'11027010LD', N'LTR', CAST(20.000 AS Decimal(18, 3)), N'BOTOL', CAST(2.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (35, N'77264100GD', N'KG', CAST(4.000 AS Decimal(18, 3)), N'SACHET', CAST(40.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (36, N'30314001LD', N'LTR', CAST(12.000 AS Decimal(18, 3)), N'BOTOL', CAST(12.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (37, N'00100500MD', N'LTR', CAST(10.000 AS Decimal(18, 3)), N'BOTOL', CAST(20.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (38, N'30898001GD', N'KG', CAST(1.000 AS Decimal(18, 3)), N'SACHET', CAST(1000.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (39, N'30889001GD', N'KG', CAST(1.000 AS Decimal(18, 3)), N'SACHET', CAST(1000.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (40, N'30896005GD', N'KG', CAST(0.500 AS Decimal(18, 3)), N'TABLET', CAST(100.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, CAST(N'2023-04-11 00:00:00' AS SmallDateTime))
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (41, N'00901100MD', N'LTR', CAST(5.000 AS Decimal(18, 3)), N'BOTOL', CAST(50.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (42, N'00901250MD', N'LTR', CAST(10.000 AS Decimal(18, 3)), N'BOTOL', CAST(40.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (43, N'00901500MD', N'LTR', CAST(10.000 AS Decimal(18, 3)), N'BOTOL', CAST(10.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (44, N'11060100MD', N'LTR', CAST(5.000 AS Decimal(18, 3)), N'BOTOL', CAST(50.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (45, N'00101020LD', N'LTR', CAST(20.000 AS Decimal(18, 3)), N'JERIGEN', CAST(1.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (46, N'11067005LE', N'LTR', CAST(20.000 AS Decimal(18, 3)), N'BOTOL', CAST(4.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (47, N'11067020LE', N'LTR', CAST(20.000 AS Decimal(18, 3)), N'JERIGEN', CAST(1.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (48, N'11032100MD', N'LTR', CAST(5.000 AS Decimal(18, 3)), N'BOTOL', CAST(50.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (49, N'11032500MD', N'LTR', CAST(10.000 AS Decimal(18, 3)), N'BOTOL', CAST(20.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (50, N'77302020LE', N'LTR', CAST(20.000 AS Decimal(18, 3)), N'JERIGEN', CAST(1.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (51, N'11028050MD', N'LTR', CAST(2.000 AS Decimal(18, 3)), N'BOTOL', CAST(40.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (52, N'11028100MD', N'LTR', CAST(2.000 AS Decimal(18, 3)), N'BOTOL', CAST(20.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (53, N'11028200MD', N'LTR', CAST(8.000 AS Decimal(18, 3)), N'BOTOL', CAST(40.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (54, N'77296025GD', N'KG', CAST(1.000 AS Decimal(18, 3)), N'SACHET', CAST(40.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (55, N'00540001LD', N'LTR', CAST(12.000 AS Decimal(18, 3)), N'BOTOL', CAST(12.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (56, N'00540004LD', N'LTR', CAST(16.000 AS Decimal(18, 3)), N'BOTOL', CAST(4.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (57, N'00540020LD', N'LTR', CAST(20.000 AS Decimal(18, 3)), N'JERIGEN', CAST(1.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (58, N'77001200MD', N'LTR', CAST(8.000 AS Decimal(18, 3)), N'BOTOL', CAST(40.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, CAST(N'2023-04-10 00:00:00' AS SmallDateTime))
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (59, N'77001500MD', N'LTR', CAST(10.000 AS Decimal(18, 3)), N'BOTOL', CAST(20.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (60, N'00010200MD', N'LTR', CAST(8.000 AS Decimal(18, 3)), N'BOTOL', CAST(40.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (61, N'00010400MD', N'LTR', CAST(8.000 AS Decimal(18, 3)), N'BOTOL', CAST(20.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (62, N'00010001LD', N'LTR', CAST(12.000 AS Decimal(18, 3)), N'BOTOL', CAST(12.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (63, N'00010020LD', N'LTR', CAST(20.000 AS Decimal(18, 3)), N'JERIGEN', CAST(1.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (64, N'00010200LD', N'LTR', CAST(200.000 AS Decimal(18, 3)), N'DRUM', CAST(1.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (65, N'11001100MD', N'LTR', CAST(5.000 AS Decimal(18, 3)), N'BOTOL', CAST(50.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (66, N'11001500MD', N'LTR', CAST(10.000 AS Decimal(18, 3)), N'BOTOL', CAST(20.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (67, N'30316500MD', N'LTR', CAST(10.000 AS Decimal(18, 3)), N'BOTOL', CAST(20.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (68, N'00096005GD', N'KG', CAST(1.000 AS Decimal(18, 3)), N'SACHET', CAST(200.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (69, N'00096500GD', N'KG', CAST(2.000 AS Decimal(18, 3)), N'BOTOL', CAST(4.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (70, N'00095005GD', N'KG', CAST(2.000 AS Decimal(18, 3)), N'SACHET', CAST(400.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (71, N'00095020GD', N'KG', CAST(1.000 AS Decimal(18, 3)), N'SACHET', CAST(50.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (72, N'00095250GD', N'KG', CAST(2.000 AS Decimal(18, 3)), N'BOTOL', CAST(8.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (73, N'77009001KD', N'KG', CAST(10.000 AS Decimal(18, 3)), N'POUCH', CAST(10.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (74, N'77266100MD', N'LTR', CAST(5.000 AS Decimal(18, 3)), N'BOTOL', CAST(50.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (75, N'77265100MD', N'LTR', CAST(5.000 AS Decimal(18, 3)), N'BOTOL', CAST(50.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (76, N'00790001LD', N'LTR', CAST(12.000 AS Decimal(18, 3)), N'BOTOL', CAST(12.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (77, N'00790004LD', N'LTR', CAST(16.000 AS Decimal(18, 3)), N'BOTOL', CAST(4.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (78, N'00798001LD', N'LTR', CAST(12.000 AS Decimal(18, 3)), N'BOTOL', CAST(12.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (79, N'00798004LD', N'LTR', CAST(16.000 AS Decimal(18, 3)), N'BOTOL', CAST(4.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (80, N'00798020LD', N'LTR', CAST(20.000 AS Decimal(18, 3)), N'JERIGEN', CAST(1.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (81, N'00075100MD', N'LTR', CAST(5.000 AS Decimal(18, 3)), N'BOTOL', CAST(50.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (82, N'00075250MD', N'LTR', CAST(10.000 AS Decimal(18, 3)), N'BOTOL', CAST(40.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (83, N'011008100M-D', N'LTR', CAST(5.000 AS Decimal(18, 3)), N'BOTOL', CAST(50.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (84, N'11008200MD', N'LTR', CAST(8.000 AS Decimal(18, 3)), N'BOTOL', CAST(40.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (85, N'11008001LD', N'LTR', CAST(15.000 AS Decimal(18, 3)), N'BOTOL', CAST(15.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (86, N'11025200LE', N'LTR', CAST(200.000 AS Decimal(18, 3)), N'DRUM', CAST(1.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (87, N'77221400MD', N'LTR', CAST(8.000 AS Decimal(18, 3)), N'BOTOL', CAST(20.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (88, N'00055001LD', N'LTR', CAST(12.000 AS Decimal(18, 3)), N'BOTOL', CAST(12.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (89, N'00055004LD', N'LTR', CAST(16.000 AS Decimal(18, 3)), N'BOTOL', CAST(4.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (90, N'00055020LD', N'LTR', CAST(20.000 AS Decimal(18, 3)), N'JERIGEN', CAST(1.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (91, N'00055200LD', N'LTR', CAST(200.000 AS Decimal(18, 3)), N'DRUM', CAST(1.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (92, N'11042100MD', N'LTR', CAST(5.000 AS Decimal(18, 3)), N'BOTOL', CAST(50.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (93, N'11042250MD', N'LTR', CAST(5.000 AS Decimal(18, 3)), N'BOTOL', CAST(20.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (94, N'11024030KD', N'KG', CAST(30.000 AS Decimal(18, 3)), N'SAK', CAST(1.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (95, N'77231001KD', N'KG', CAST(10.000 AS Decimal(18, 3)), N'POUCH', CAST(10.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (96, N'77018400GD', N'KG', CAST(10.000 AS Decimal(18, 3)), N'SACHET', CAST(25.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (97, N'00043001LD', N'LTR', CAST(12.000 AS Decimal(18, 3)), N'BOTOL', CAST(12.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (98, N'00043004LD', N'LTR', CAST(16.000 AS Decimal(18, 3)), N'BOTOL', CAST(4.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (99, N'00053001LD', N'LTR', CAST(15.000 AS Decimal(18, 3)), N'BOTOL', CAST(15.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (100, N'00011200MD', N'LTR', CAST(8.000 AS Decimal(18, 3)), N'BOTOL', CAST(40.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (101, N'00011400MD', N'LTR', CAST(8.000 AS Decimal(18, 3)), N'BOTOL', CAST(20.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (102, N'00011200LD', N'LTR', CAST(200.000 AS Decimal(18, 3)), N'DRUM', CAST(1.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (103, N'00681001LD', N'LTR', CAST(12.000 AS Decimal(18, 3)), N'BOTOL', CAST(12.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (104, N'00684004LD', N'LTR', CAST(16.000 AS Decimal(18, 3)), N'BOTOL', CAST(4.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (105, N'006820020LD', N'LTR', CAST(20.000 AS Decimal(18, 3)), N'JERIGEN', CAST(1.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (106, N'0060200200MD', N'LTR', CAST(10.000 AS Decimal(18, 3)), N'BOTOL', CAST(50.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (107, N'00601001LD', N'LTR', CAST(12.000 AS Decimal(18, 3)), N'BOTOL', CAST(12.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (108, N'00604040LD', N'LTR', CAST(16.000 AS Decimal(18, 3)), N'BOTOL', CAST(4.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (109, N'006020020LD', N'LTR', CAST(20.000 AS Decimal(18, 3)), N'JERIGEN', CAST(1.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (110, N'007801001LD', N'LTR', CAST(10.000 AS Decimal(18, 3)), N'BOTOL', CAST(50.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (111, N'007804004LD', N'LTR', CAST(12.000 AS Decimal(18, 3)), N'BOTOL', CAST(12.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (112, N'007820020LD', N'LTR', CAST(16.000 AS Decimal(18, 3)), N'BOTOL', CAST(4.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (113, N'0078200200MD', N'LTR', CAST(20.000 AS Decimal(18, 3)), N'JERIGEN', CAST(1.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (114, N'00903025GD', N'KG', CAST(2.000 AS Decimal(18, 3)), N'SACHET', CAST(80.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (115, N'11030025GD', N'KG', CAST(4.000 AS Decimal(18, 3)), N'SACHET', CAST(160.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (116, N'77290080MD', N'LTR', CAST(4.000 AS Decimal(18, 3)), N'BOTOL', CAST(50.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (117, N'77290250MD', N'LTR', CAST(10.000 AS Decimal(18, 3)), N'BOTOL', CAST(40.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (118, N'00793001LD', N'LTR', CAST(12.000 AS Decimal(18, 3)), N'BOTOL', CAST(12.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (119, N'00793004LD', N'LTR', CAST(16.000 AS Decimal(18, 3)), N'BOTOL', CAST(4.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (120, N'77294400MD', N'LTR', CAST(8.000 AS Decimal(18, 3)), N'BOTOL', CAST(20.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (121, N'00070100MD', N'LTR', CAST(5.000 AS Decimal(18, 3)), N'BOTOL', CAST(50.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (122, N'00070001LD', N'LTR', CAST(15.000 AS Decimal(18, 3)), N'BOTOL', CAST(15.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (123, N'00070004LD', N'LTR', CAST(16.000 AS Decimal(18, 3)), N'BOTOL', CAST(4.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (124, N'77278100GD', N'KG', CAST(4.000 AS Decimal(18, 3)), N'SACHET', CAST(40.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (125, N'77300100MD', N'LTR', CAST(10.000 AS Decimal(18, 3)), N'BOTOL', CAST(100.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (126, N'77300200MD', N'LTR', CAST(8.000 AS Decimal(18, 3)), N'BOTOL', CAST(40.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (127, N'77300020LD', N'LTR', CAST(20.000 AS Decimal(18, 3)), N'JERIGEN', CAST(1.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (128, N'77277100GD', N'KG', CAST(4.000 AS Decimal(18, 3)), N'SACHET', CAST(40.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (129, N'00083100GD', N'KG', CAST(4.000 AS Decimal(18, 3)), N'BOTOL', CAST(40.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (130, N'00083500GD', N'KG', CAST(7.500 AS Decimal(18, 3)), N'BOTOL', CAST(15.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (131, N'00098020LD', N'LTR', CAST(20.000 AS Decimal(18, 3)), N'JERIGEN', CAST(1.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (132, N'00900200MD', N'LTR', CAST(4.000 AS Decimal(18, 3)), N'BOTOL', CAST(20.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (133, N'11064100MD', N'LTR', CAST(10.000 AS Decimal(18, 3)), N'BOTOL', CAST(100.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (134, N'11064250M-D', N'LTR', CAST(10.000 AS Decimal(18, 3)), N'BOTOL', CAST(40.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (135, N'30319200LE', N'LTR', CAST(200.000 AS Decimal(18, 3)), N'DRUM', CAST(1.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (136, N'11046500MD', N'LTR', CAST(10.000 AS Decimal(18, 3)), N'BOTOL', CAST(20.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (137, N'11046001LD', N'LTR', CAST(12.000 AS Decimal(18, 3)), N'BOTOL', CAST(12.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (138, N'00108200GD', N'KG', CAST(1.600 AS Decimal(18, 3)), N'BOTOL', CAST(8.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (139, N'00108500GD', N'KG', CAST(3.000 AS Decimal(18, 3)), N'BOTOL', CAST(6.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (140, N'00108500GE', N'KG', CAST(3.000 AS Decimal(18, 3)), N'BOTOL', CAST(6.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (141, N'00210500MD', N'LTR', CAST(10.000 AS Decimal(18, 3)), N'BOTOL', CAST(20.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (142, N'77240001KD', N'KG', CAST(10.000 AS Decimal(18, 3)), N'POUCH', CAST(10.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (143, N'11065500GD', N'KG', CAST(12.000 AS Decimal(18, 3)), N'BOTOL', CAST(24.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (144, N'07230500GD', N'KG', CAST(10.000 AS Decimal(18, 3)), N'POUCH', CAST(20.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (145, N'77230001KD', N'KG', CAST(10.000 AS Decimal(18, 3)), N'POUCH', CAST(10.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (146, N'11029100MD', N'LTR', CAST(5.000 AS Decimal(18, 3)), N'BOTOL', CAST(50.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (147, N'77301250MD', N'LTR', CAST(10.000 AS Decimal(18, 3)), N'BOTOL', CAST(40.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[BRND_PROD_CONV] ([IDApp], [BRANDPACK_ID], [UNIT1], [VOL1], [UNIT2], [VOL2], [INACTIVE], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (148, N'77252001KD', N'KG', CAST(12.000 AS Decimal(18, 3)), N'POUCH', CAST(12.000 AS Decimal(18, 3)), 0, N'System Adminisitrator', CAST(N'2023-01-04 00:00:00' AS SmallDateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[BRND_PROD_CONV] OFF
GO
SET IDENTITY_INSERT [dbo].[GON_SEPARATED_DETAIL] ON 

GO
INSERT [dbo].[GON_SEPARATED_DETAIL] ([IDApp], [FKAppGonHeader], [FKAppPODetail], [ITEM], [QTY], [COLLY_BOX], [COLLY_PACKSIZE], [BATCH_NO], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (2, 6, 3, N'77018400GD', CAST(1234.0000 AS Decimal(18, 4)), N'123 BOX', N'10 SACHET', CAST(967897 AS Numeric(18, 0)), N'System Administrator', CAST(N'2023-01-24 00:00:00' AS SmallDateTime), N'System Administrator', CAST(N'2023-02-10 00:00:00' AS SmallDateTime))
GO
INSERT [dbo].[GON_SEPARATED_DETAIL] ([IDApp], [FKAppGonHeader], [FKAppPODetail], [ITEM], [QTY], [COLLY_BOX], [COLLY_PACKSIZE], [BATCH_NO], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (26, 6, 1, N'11001500MD', CAST(3245.0000 AS Decimal(18, 4)), N'324 BOX', N'10 BOTOL', NULL, N'', CAST(N'2023-02-08 00:00:00' AS SmallDateTime), N'System Administrator', CAST(N'2023-02-10 00:00:00' AS SmallDateTime))
GO
INSERT [dbo].[GON_SEPARATED_DETAIL] ([IDApp], [FKAppGonHeader], [FKAppPODetail], [ITEM], [QTY], [COLLY_BOX], [COLLY_PACKSIZE], [BATCH_NO], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (27, 6, 25, N'77301250MD', CAST(2.0000 AS Decimal(18, 4)), N'', N'8 BOTOL', NULL, N'', CAST(N'2023-02-08 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_DETAIL] ([IDApp], [FKAppGonHeader], [FKAppPODetail], [ITEM], [QTY], [COLLY_BOX], [COLLY_PACKSIZE], [BATCH_NO], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (28, 6, 26, N'77290080MD', CAST(104.0000 AS Decimal(18, 4)), N'26 BOX', N'', NULL, N'', CAST(N'2023-02-08 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_DETAIL] ([IDApp], [FKAppGonHeader], [FKAppPODetail], [ITEM], [QTY], [COLLY_BOX], [COLLY_PACKSIZE], [BATCH_NO], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (35, 6, 28, N'30898001GD', CAST(2345.0000 AS Decimal(18, 4)), N'2345 BOX', N'', NULL, N'', CAST(N'2023-02-10 00:00:00' AS SmallDateTime), N'System Administrator', CAST(N'2023-02-10 00:00:00' AS SmallDateTime))
GO
INSERT [dbo].[GON_SEPARATED_DETAIL] ([IDApp], [FKAppGonHeader], [FKAppPODetail], [ITEM], [QTY], [COLLY_BOX], [COLLY_PACKSIZE], [BATCH_NO], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (37, 6, 27, N'77290250MD', CAST(120.0000 AS Decimal(18, 4)), N'12 BOX', N'', NULL, N'', CAST(N'2023-02-10 00:00:00' AS SmallDateTime), N'System Administrator', CAST(N'2023-02-10 00:00:00' AS SmallDateTime))
GO
INSERT [dbo].[GON_SEPARATED_DETAIL] ([IDApp], [FKAppGonHeader], [FKAppPODetail], [ITEM], [QTY], [COLLY_BOX], [COLLY_PACKSIZE], [BATCH_NO], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (38, 16, 1, N'11001500MD', CAST(3025.0000 AS Decimal(18, 4)), N'302 BOX', N'10 BOTOL', NULL, N'System Administrator', CAST(N'2023-02-13 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_DETAIL] ([IDApp], [FKAppGonHeader], [FKAppPODetail], [ITEM], [QTY], [COLLY_BOX], [COLLY_PACKSIZE], [BATCH_NO], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (39, 16, 3, N'77018400GD', CAST(166.0000 AS Decimal(18, 4)), N'16 BOX', N'15 SACHET', NULL, N'System Administrator', CAST(N'2023-02-13 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_DETAIL] ([IDApp], [FKAppGonHeader], [FKAppPODetail], [ITEM], [QTY], [COLLY_BOX], [COLLY_PACKSIZE], [BATCH_NO], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (40, 16, 27, N'77290250MD', CAST(244.0000 AS Decimal(18, 4)), N'24 BOX', N'16 BOTOL', NULL, N'System Administrator', CAST(N'2023-02-13 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_DETAIL] ([IDApp], [FKAppGonHeader], [FKAppPODetail], [ITEM], [QTY], [COLLY_BOX], [COLLY_PACKSIZE], [BATCH_NO], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (41, 16, 28, N'30898001GD', CAST(47293.0000 AS Decimal(18, 4)), N'47293 BOX', N'', NULL, N'System Administrator', CAST(N'2023-02-13 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_DETAIL] ([IDApp], [FKAppGonHeader], [FKAppPODetail], [ITEM], [QTY], [COLLY_BOX], [COLLY_PACKSIZE], [BATCH_NO], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (69, 24, 59, N'00043001LD', CAST(600.0000 AS Decimal(18, 4)), N'50 BOX', N'', CAST(206000 AS Numeric(18, 0)), N'System Administrator', CAST(N'2023-02-21 00:00:00' AS SmallDateTime), NULL, CAST(N'2023-02-22 00:00:00' AS SmallDateTime))
GO
INSERT [dbo].[GON_SEPARATED_DETAIL] ([IDApp], [FKAppGonHeader], [FKAppPODetail], [ITEM], [QTY], [COLLY_BOX], [COLLY_PACKSIZE], [BATCH_NO], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (70, 24, 63, N'00070001LD', CAST(1200.0000 AS Decimal(18, 4)), N'80 BOX', N'', CAST(23400 AS Numeric(18, 0)), N'System Administrator', CAST(N'2023-02-21 00:00:00' AS SmallDateTime), NULL, CAST(N'2023-02-22 00:00:00' AS SmallDateTime))
GO
INSERT [dbo].[GON_SEPARATED_DETAIL] ([IDApp], [FKAppGonHeader], [FKAppPODetail], [ITEM], [QTY], [COLLY_BOX], [COLLY_PACKSIZE], [BATCH_NO], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (71, 24, 60, N'00075100MD', CAST(200.0000 AS Decimal(18, 4)), N'40 BOX', N'', CAST(24000 AS Numeric(18, 0)), N'System Administrator', CAST(N'2023-02-21 00:00:00' AS SmallDateTime), N'System Administrator', CAST(N'2023-02-22 00:00:00' AS SmallDateTime))
GO
INSERT [dbo].[GON_SEPARATED_DETAIL] ([IDApp], [FKAppGonHeader], [FKAppPODetail], [ITEM], [QTY], [COLLY_BOX], [COLLY_PACKSIZE], [BATCH_NO], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (72, 24, 61, N'77252001KD', CAST(1574.0000 AS Decimal(18, 4)), N'131 BOX', N'2 POUCH', CAST(100012 AS Numeric(18, 0)), N'System Administrator', CAST(N'2023-02-21 00:00:00' AS SmallDateTime), NULL, CAST(N'2023-02-22 00:00:00' AS SmallDateTime))
GO
INSERT [dbo].[GON_SEPARATED_DETAIL] ([IDApp], [FKAppGonHeader], [FKAppPODetail], [ITEM], [QTY], [COLLY_BOX], [COLLY_PACKSIZE], [BATCH_NO], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (74, 25, 60, N'00075100MD', CAST(300.0000 AS Decimal(18, 4)), N'60 BOX', N'', NULL, N'System Administrator', CAST(N'2023-02-22 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_DETAIL] ([IDApp], [FKAppGonHeader], [FKAppPODetail], [ITEM], [QTY], [COLLY_BOX], [COLLY_PACKSIZE], [BATCH_NO], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (75, 25, 62, N'77290250MD', CAST(240.0000 AS Decimal(18, 4)), N'24 BOX', N'', NULL, N'System Administrator', CAST(N'2023-02-22 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_DETAIL] ([IDApp], [FKAppGonHeader], [FKAppPODetail], [ITEM], [QTY], [COLLY_BOX], [COLLY_PACKSIZE], [BATCH_NO], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (127, 30, 80, N'30314001LD', CAST(1.0000 AS Decimal(18, 4)), N'', N'1 BOTOL', NULL, N'', CAST(N'2023-03-23 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_DETAIL] ([IDApp], [FKAppGonHeader], [FKAppPODetail], [ITEM], [QTY], [COLLY_BOX], [COLLY_PACKSIZE], [BATCH_NO], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (128, 30, 81, N'011009100M-D', CAST(2.5000 AS Decimal(18, 4)), N'', N'25 BOTOL', NULL, N'', CAST(N'2023-03-23 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_DETAIL] ([IDApp], [FKAppGonHeader], [FKAppPODetail], [ITEM], [QTY], [COLLY_BOX], [COLLY_PACKSIZE], [BATCH_NO], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (129, 30, 82, N'11060100MD', CAST(1.5000 AS Decimal(18, 4)), N'', N'15 BOTOL', NULL, N'', CAST(N'2023-03-23 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_DETAIL] ([IDApp], [FKAppGonHeader], [FKAppPODetail], [ITEM], [QTY], [COLLY_BOX], [COLLY_PACKSIZE], [BATCH_NO], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (130, 30, 83, N'077001200MD', CAST(2.0000 AS Decimal(18, 4)), N'', N'10 BOTOL', NULL, N'', CAST(N'2023-03-23 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_DETAIL] ([IDApp], [FKAppGonHeader], [FKAppPODetail], [ITEM], [QTY], [COLLY_BOX], [COLLY_PACKSIZE], [BATCH_NO], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (131, 30, 84, N'30889001GD', CAST(0.4000 AS Decimal(18, 4)), N'', N'400 SACHET', NULL, N'', CAST(N'2023-03-23 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_DETAIL] ([IDApp], [FKAppGonHeader], [FKAppPODetail], [ITEM], [QTY], [COLLY_BOX], [COLLY_PACKSIZE], [BATCH_NO], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (136, 37, 83, N'077001200MD', CAST(1.0000 AS Decimal(18, 4)), N'', N'5 BOTOL', NULL, N'System Administrator', CAST(N'2023-03-27 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_DETAIL] ([IDApp], [FKAppGonHeader], [FKAppPODetail], [ITEM], [QTY], [COLLY_BOX], [COLLY_PACKSIZE], [BATCH_NO], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (138, 38, 74, N'00540001LD', CAST(2.0000 AS Decimal(18, 4)), N'', N'2 BOTOL', NULL, N'System Administrator', CAST(N'2023-03-27 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_DETAIL] ([IDApp], [FKAppGonHeader], [FKAppPODetail], [ITEM], [QTY], [COLLY_BOX], [COLLY_PACKSIZE], [BATCH_NO], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (139, 38, 79, N'00095020GD', CAST(1.0000 AS Decimal(18, 4)), N'1 BOX', N'', NULL, N'System Administrator', CAST(N'2023-03-27 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_DETAIL] ([IDApp], [FKAppGonHeader], [FKAppPODetail], [ITEM], [QTY], [COLLY_BOX], [COLLY_PACKSIZE], [BATCH_NO], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (140, 39, 73, N'00681001LD', CAST(2.5000 AS Decimal(18, 4)), N'', N'2 BOTOL', NULL, N'aras', CAST(N'2023-03-27 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_DETAIL] ([IDApp], [FKAppGonHeader], [FKAppPODetail], [ITEM], [QTY], [COLLY_BOX], [COLLY_PACKSIZE], [BATCH_NO], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (141, 39, 74, N'00540001LD', CAST(13.0000 AS Decimal(18, 4)), N'1 BOX', N'1 BOTOL', NULL, N'aras', CAST(N'2023-03-27 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_DETAIL] ([IDApp], [FKAppGonHeader], [FKAppPODetail], [ITEM], [QTY], [COLLY_BOX], [COLLY_PACKSIZE], [BATCH_NO], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (142, 39, 75, N'00798001LD', CAST(5.0000 AS Decimal(18, 4)), N'', N'5 BOTOL', NULL, N'aras', CAST(N'2023-03-27 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_DETAIL] ([IDApp], [FKAppGonHeader], [FKAppPODetail], [ITEM], [QTY], [COLLY_BOX], [COLLY_PACKSIZE], [BATCH_NO], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (145, 40, 76, N'00070100MD', CAST(10.0000 AS Decimal(18, 4)), N'2 BOX', N'', NULL, N'aras', CAST(N'2023-03-28 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_DETAIL] ([IDApp], [FKAppGonHeader], [FKAppPODetail], [ITEM], [QTY], [COLLY_BOX], [COLLY_PACKSIZE], [BATCH_NO], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (146, 40, 78, N'00083500GD', CAST(2.0000 AS Decimal(18, 4)), N'', N'4 BOTOL', NULL, N'aras', CAST(N'2023-03-28 00:00:00' AS SmallDateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[GON_SEPARATED_DETAIL] OFF
GO
SET IDENTITY_INSERT [dbo].[GON_SEPARATED_HEADER] ON 

GO
INSERT [dbo].[GON_SEPARATED_HEADER] ([IDApp], [FKApp], [GON_NUMBER], [GON_DATE], [WARHOUSE], [SHIP_TO], [SHIP_TO_CUST], [SHIP_TO_ADDRESS], [TRANSPORTER], [POLICE_NO_TRANS], [DRIVER_TRANS], [GON_AREA], [REMARK], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (6, 7, N'22/23000194-01', CAST(N'2023-01-24 00:00:00' AS SmallDateTime), N'JKT', N'END001IDR', N'', N'', N'0001', N'FGBFG-7686', N'FGHFG-5675', N'008', N'', CAST(N'2023-01-24 00:00:00' AS SmallDateTime), N'System Administrator', CAST(N'2023-02-07 00:00:00' AS SmallDateTime), N'System Administrator')
GO
INSERT [dbo].[GON_SEPARATED_HEADER] ([IDApp], [FKApp], [GON_NUMBER], [GON_DATE], [WARHOUSE], [SHIP_TO], [SHIP_TO_CUST], [SHIP_TO_ADDRESS], [TRANSPORTER], [POLICE_NO_TRANS], [DRIVER_TRANS], [GON_AREA], [REMARK], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (16, 7, N'10.88745-0', CAST(N'2023-02-13 00:00:00' AS SmallDateTime), N'JKT', N'END001IDR', N'', N'', N'0001', N'0347638-324', N'874783543', N'006', N'', CAST(N'2023-02-13 00:00:00' AS SmallDateTime), N'System Administrator', NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_HEADER] ([IDApp], [FKApp], [GON_NUMBER], [GON_DATE], [WARHOUSE], [SHIP_TO], [SHIP_TO_CUST], [SHIP_TO_ADDRESS], [TRANSPORTER], [POLICE_NO_TRANS], [DRIVER_TRANS], [GON_AREA], [REMARK], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (24, 17, N'22/23 001406 -02', CAST(N'2023-02-10 00:00:00' AS SmallDateTime), N'SBY', NULL, N'PT. Nufarm Indonesia', N'PERGUDANGAN PT. BGR LOGISTIC JL KESTRIAN,NO 22 A DESA SIDOKERTO KEC BUDURAN,KAB SIDOARJO JAWA TIMUR', N'0039', N'C8713ZN89123', N'JAMINGUN', N'010', N'', CAST(N'2023-02-21 00:00:00' AS SmallDateTime), N'System Administrator', CAST(N'2023-02-22 00:00:00' AS SmallDateTime), N'System Administrator')
GO
INSERT [dbo].[GON_SEPARATED_HEADER] ([IDApp], [FKApp], [GON_NUMBER], [GON_DATE], [WARHOUSE], [SHIP_TO], [SHIP_TO_CUST], [SHIP_TO_ADDRESS], [TRANSPORTER], [POLICE_NO_TRANS], [DRIVER_TRANS], [GON_AREA], [REMARK], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (25, 17, N'5464564', CAST(N'2023-02-13 00:00:00' AS SmallDateTime), N'JKT', NULL, N'PT. Nufarm Indonesia', N'PERGUDANGAN PT. BGR LOGISTIC JL KESTRIAN,NO 22 A DESA SIDOKERTO KEC BUDURAN,KAB SIDOARJO JAWA TIMUR', N'0039', N'C8713ZN', N'JAMINGUN', N'010', N'', CAST(N'2023-02-22 00:00:00' AS SmallDateTime), N'System Administrator', NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_HEADER] ([IDApp], [FKApp], [GON_NUMBER], [GON_DATE], [WARHOUSE], [SHIP_TO], [SHIP_TO_CUST], [SHIP_TO_ADDRESS], [TRANSPORTER], [POLICE_NO_TRANS], [DRIVER_TRANS], [GON_AREA], [REMARK], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (30, 20, N'12345', CAST(N'2023-02-08 00:00:00' AS SmallDateTime), N'JKT', N'DON001IDR', N'', N'', N'0044', N'B 9319 UYW', N'JUNAIDI', N'001', N'', CAST(N'2023-03-21 00:00:00' AS SmallDateTime), N'System Administrator', NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_HEADER] ([IDApp], [FKApp], [GON_NUMBER], [GON_DATE], [WARHOUSE], [SHIP_TO], [SHIP_TO_CUST], [SHIP_TO_ADDRESS], [TRANSPORTER], [POLICE_NO_TRANS], [DRIVER_TRANS], [GON_AREA], [REMARK], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (37, 20, N'34354', CAST(N'2023-02-08 00:00:00' AS SmallDateTime), N'JKT', N'DON001IDR', N'', N'', N'0044', N'DS', N'JAMINGUN', N'001', N'', CAST(N'2023-03-27 00:00:00' AS SmallDateTime), N'System Administrator', NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_HEADER] ([IDApp], [FKApp], [GON_NUMBER], [GON_DATE], [WARHOUSE], [SHIP_TO], [SHIP_TO_CUST], [SHIP_TO_ADDRESS], [TRANSPORTER], [POLICE_NO_TRANS], [DRIVER_TRANS], [GON_AREA], [REMARK], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (38, 19, N'67890', CAST(N'2023-03-08 00:00:00' AS SmallDateTime), N'MRK', N'TAN012IDR', N'', N'', N'0044', N'FGBFG-7686', N'JAMINGUN', N'020', N'', CAST(N'2023-03-27 00:00:00' AS SmallDateTime), N'System Administrator', NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_HEADER] ([IDApp], [FKApp], [GON_NUMBER], [GON_DATE], [WARHOUSE], [SHIP_TO], [SHIP_TO_CUST], [SHIP_TO_ADDRESS], [TRANSPORTER], [POLICE_NO_TRANS], [DRIVER_TRANS], [GON_AREA], [REMARK], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (39, 19, N'097906', CAST(N'2023-03-08 00:00:00' AS SmallDateTime), N'MRK', N'TAN012IDR', N'', N'', N'0044', N'FGBFG-7686', N'JAMINGUN', N'001', N'', CAST(N'2023-03-27 00:00:00' AS SmallDateTime), N'aras', NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_HEADER] ([IDApp], [FKApp], [GON_NUMBER], [GON_DATE], [WARHOUSE], [SHIP_TO], [SHIP_TO_CUST], [SHIP_TO_ADDRESS], [TRANSPORTER], [POLICE_NO_TRANS], [DRIVER_TRANS], [GON_AREA], [REMARK], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (40, 19, N'980689', CAST(N'2023-03-08 00:00:00' AS SmallDateTime), N'MRK', N'TAN012IDR', N'', N'', N'0044', N'FGBFG-7686', N'JAMINGUN', N'001', N'', CAST(N'2023-03-28 00:00:00' AS SmallDateTime), N'aras', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[GON_SEPARATED_HEADER] OFF
GO
SET IDENTITY_INSERT [dbo].[GON_SEPARATED_PO_DETAIL] ON 

GO
INSERT [dbo].[GON_SEPARATED_PO_DETAIL] ([IDApp], [FKApp], [BRANDPACK_ID], [QUANTITY], [STATUS], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (1, 7, N'11001500MD', CAST(6270.000 AS Decimal(18, 3)), N'Completed', N'System Administrator', CAST(N'2023-01-24 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_PO_DETAIL] ([IDApp], [FKApp], [BRANDPACK_ID], [QUANTITY], [STATUS], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (3, 7, N'77018400GD', CAST(1400.000 AS Decimal(18, 3)), N'Completed', N'System Administrator', CAST(N'2023-01-24 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_PO_DETAIL] ([IDApp], [FKApp], [BRANDPACK_ID], [QUANTITY], [STATUS], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (25, 7, N'77301250MD', CAST(2.000 AS Decimal(18, 3)), N'Completed', N'', CAST(N'2023-02-07 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_PO_DETAIL] ([IDApp], [FKApp], [BRANDPACK_ID], [QUANTITY], [STATUS], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (26, 7, N'77290080MD', CAST(104.000 AS Decimal(18, 3)), N'Completed', N'', CAST(N'2023-02-07 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_PO_DETAIL] ([IDApp], [FKApp], [BRANDPACK_ID], [QUANTITY], [STATUS], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (27, 7, N'77290250MD', CAST(364.000 AS Decimal(18, 3)), N'Completed', N'', CAST(N'2023-02-07 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_PO_DETAIL] ([IDApp], [FKApp], [BRANDPACK_ID], [QUANTITY], [STATUS], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (28, 7, N'30898001GD', CAST(49638.000 AS Decimal(18, 3)), N'Completed', N'', CAST(N'2023-02-07 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_PO_DETAIL] ([IDApp], [FKApp], [BRANDPACK_ID], [QUANTITY], [STATUS], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (59, 17, N'00043001LD', CAST(600.000 AS Decimal(18, 3)), N'Completed', N'System Administrator', CAST(N'2023-02-20 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_PO_DETAIL] ([IDApp], [FKApp], [BRANDPACK_ID], [QUANTITY], [STATUS], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (60, 17, N'00075100MD', CAST(500.000 AS Decimal(18, 3)), N'Completed', N'System Administrator', CAST(N'2023-02-20 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_PO_DETAIL] ([IDApp], [FKApp], [BRANDPACK_ID], [QUANTITY], [STATUS], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (61, 17, N'77252001KD', CAST(1574.000 AS Decimal(18, 3)), N'Completed', N'System Administrator', CAST(N'2023-02-20 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_PO_DETAIL] ([IDApp], [FKApp], [BRANDPACK_ID], [QUANTITY], [STATUS], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (62, 17, N'77290250MD', CAST(240.000 AS Decimal(18, 3)), N'Completed', N'System Administrator', CAST(N'2023-02-20 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_PO_DETAIL] ([IDApp], [FKApp], [BRANDPACK_ID], [QUANTITY], [STATUS], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (63, 17, N'00070001LD', CAST(1200.000 AS Decimal(18, 3)), N'Completed', N'System Administrator', CAST(N'2023-02-20 00:00:00' AS SmallDateTime), NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_PO_DETAIL] ([IDApp], [FKApp], [BRANDPACK_ID], [QUANTITY], [STATUS], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (73, 19, N'00681001LD', CAST(2.500 AS Decimal(18, 3)), N'Completed', N'System Administrator', CAST(N'2023-03-08 00:00:00' AS SmallDateTime), N'aras', CAST(N'2023-03-28 00:00:00' AS SmallDateTime))
GO
INSERT [dbo].[GON_SEPARATED_PO_DETAIL] ([IDApp], [FKApp], [BRANDPACK_ID], [QUANTITY], [STATUS], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (74, 19, N'00540001LD', CAST(15.000 AS Decimal(18, 3)), N'Completed', N'System Administrator', CAST(N'2023-03-08 00:00:00' AS SmallDateTime), N'System Administrator', CAST(N'2023-03-28 00:00:00' AS SmallDateTime))
GO
INSERT [dbo].[GON_SEPARATED_PO_DETAIL] ([IDApp], [FKApp], [BRANDPACK_ID], [QUANTITY], [STATUS], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (75, 19, N'00798001LD', CAST(5.000 AS Decimal(18, 3)), N'Completed', N'System Administrator', CAST(N'2023-03-08 00:00:00' AS SmallDateTime), N'System Administrator', CAST(N'2023-03-28 00:00:00' AS SmallDateTime))
GO
INSERT [dbo].[GON_SEPARATED_PO_DETAIL] ([IDApp], [FKApp], [BRANDPACK_ID], [QUANTITY], [STATUS], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (76, 19, N'00070100MD', CAST(10.000 AS Decimal(18, 3)), N'Completed', N'System Administrator', CAST(N'2023-03-08 00:00:00' AS SmallDateTime), N'System Administrator', CAST(N'2023-03-28 00:00:00' AS SmallDateTime))
GO
INSERT [dbo].[GON_SEPARATED_PO_DETAIL] ([IDApp], [FKApp], [BRANDPACK_ID], [QUANTITY], [STATUS], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (78, 19, N'00083500GD', CAST(2.000 AS Decimal(18, 3)), N'Completed', N'System Administrator', CAST(N'2023-03-08 00:00:00' AS SmallDateTime), N'System Administrator', CAST(N'2023-03-28 00:00:00' AS SmallDateTime))
GO
INSERT [dbo].[GON_SEPARATED_PO_DETAIL] ([IDApp], [FKApp], [BRANDPACK_ID], [QUANTITY], [STATUS], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (79, 19, N'00095020GD', CAST(1.000 AS Decimal(18, 3)), N'Completed', N'System Administrator', CAST(N'2023-03-08 00:00:00' AS SmallDateTime), NULL, CAST(N'2023-03-28 00:00:00' AS SmallDateTime))
GO
INSERT [dbo].[GON_SEPARATED_PO_DETAIL] ([IDApp], [FKApp], [BRANDPACK_ID], [QUANTITY], [STATUS], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (80, 20, N'30314001LD', CAST(1.000 AS Decimal(18, 3)), N'Completed', N'System Administrator', CAST(N'2023-03-08 00:00:00' AS SmallDateTime), N'System Administrator', CAST(N'2023-03-27 00:00:00' AS SmallDateTime))
GO
INSERT [dbo].[GON_SEPARATED_PO_DETAIL] ([IDApp], [FKApp], [BRANDPACK_ID], [QUANTITY], [STATUS], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (81, 20, N'011009100M-D', CAST(2.500 AS Decimal(18, 3)), N'Completed', N'System Administrator', CAST(N'2023-03-08 00:00:00' AS SmallDateTime), N'System Administrator', CAST(N'2023-03-27 00:00:00' AS SmallDateTime))
GO
INSERT [dbo].[GON_SEPARATED_PO_DETAIL] ([IDApp], [FKApp], [BRANDPACK_ID], [QUANTITY], [STATUS], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (82, 20, N'11060100MD', CAST(1.500 AS Decimal(18, 3)), N'Completed', N'System Administrator', CAST(N'2023-03-08 00:00:00' AS SmallDateTime), N'System Administrator', CAST(N'2023-03-27 00:00:00' AS SmallDateTime))
GO
INSERT [dbo].[GON_SEPARATED_PO_DETAIL] ([IDApp], [FKApp], [BRANDPACK_ID], [QUANTITY], [STATUS], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (83, 20, N'077001200MD', CAST(3.000 AS Decimal(18, 3)), N'Completed', N'System Administrator', CAST(N'2023-03-08 00:00:00' AS SmallDateTime), N'System Administrator', CAST(N'2023-03-27 00:00:00' AS SmallDateTime))
GO
INSERT [dbo].[GON_SEPARATED_PO_DETAIL] ([IDApp], [FKApp], [BRANDPACK_ID], [QUANTITY], [STATUS], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate]) VALUES (84, 20, N'30889001GD', CAST(0.400 AS Decimal(18, 3)), N'Completed', N'System Administrator', CAST(N'2023-03-08 00:00:00' AS SmallDateTime), N'System Administrator', CAST(N'2023-03-27 00:00:00' AS SmallDateTime))
GO
SET IDENTITY_INSERT [dbo].[GON_SEPARATED_PO_DETAIL] OFF
GO
SET IDENTITY_INSERT [dbo].[GON_SEPARATED_PO_HEADER] ON 

GO
INSERT [dbo].[GON_SEPARATED_PO_HEADER] ([IDApp], [PO_NUMBER], [PO_DATE], [SPPB_NUMBER], [SPPB_DATE], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (7, N'R20220118A1', CAST(N'2023-01-24 00:00:00' AS SmallDateTime), N'SFSDFSFSD', CAST(N'2023-01-24 00:00:00' AS SmallDateTime), CAST(N'2023-01-24 00:00:00' AS SmallDateTime), N'System Administrator', NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_PO_HEADER] ([IDApp], [PO_NUMBER], [PO_DATE], [SPPB_NUMBER], [SPPB_DATE], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (17, N'TRANSFER', CAST(N'2023-02-10 00:00:00' AS SmallDateTime), N'TS000131', CAST(N'2023-02-10 00:00:00' AS SmallDateTime), CAST(N'2023-02-20 00:00:00' AS SmallDateTime), N'System Administrator', NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_PO_HEADER] ([IDApp], [PO_NUMBER], [PO_DATE], [SPPB_NUMBER], [SPPB_DATE], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (19, N'SM/S1/02/0184', CAST(N'2023-03-08 00:00:00' AS SmallDateTime), N'SS0184', CAST(N'2023-03-08 00:00:00' AS SmallDateTime), CAST(N'2023-03-08 00:00:00' AS SmallDateTime), N'System Administrator', NULL, NULL)
GO
INSERT [dbo].[GON_SEPARATED_PO_HEADER] ([IDApp], [PO_NUMBER], [PO_DATE], [SPPB_NUMBER], [SPPB_DATE], [CreatedDate], [CreatedBy], [ModifiedDate], [ModifiedBy]) VALUES (20, N'SM/S1/02/0185-02', CAST(N'2023-02-08 00:00:00' AS SmallDateTime), N'SS0185', CAST(N'2023-02-08 00:00:00' AS SmallDateTime), CAST(N'2023-03-08 00:00:00' AS SmallDateTime), N'System Administrator', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[GON_SEPARATED_PO_HEADER] OFF
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GON_SEPARATED_DETAIL_BRND_BRANDPACK]') AND parent_object_id = OBJECT_ID(N'[dbo].[GON_SEPARATED_DETAIL]'))
ALTER TABLE [dbo].[GON_SEPARATED_DETAIL]  WITH CHECK ADD  CONSTRAINT [FK_GON_SEPARATED_DETAIL_BRND_BRANDPACK] FOREIGN KEY([ITEM])
REFERENCES [dbo].[BRND_BRANDPACK] ([BRANDPACK_ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GON_SEPARATED_DETAIL_BRND_BRANDPACK]') AND parent_object_id = OBJECT_ID(N'[dbo].[GON_SEPARATED_DETAIL]'))
ALTER TABLE [dbo].[GON_SEPARATED_DETAIL] CHECK CONSTRAINT [FK_GON_SEPARATED_DETAIL_BRND_BRANDPACK]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GON_SEPARATED_DETAIL_GON_SEPARATED_HEADER]') AND parent_object_id = OBJECT_ID(N'[dbo].[GON_SEPARATED_DETAIL]'))
ALTER TABLE [dbo].[GON_SEPARATED_DETAIL]  WITH CHECK ADD  CONSTRAINT [FK_GON_SEPARATED_DETAIL_GON_SEPARATED_HEADER] FOREIGN KEY([FKAppGonHeader])
REFERENCES [dbo].[GON_SEPARATED_HEADER] ([IDApp])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GON_SEPARATED_DETAIL_GON_SEPARATED_HEADER]') AND parent_object_id = OBJECT_ID(N'[dbo].[GON_SEPARATED_DETAIL]'))
ALTER TABLE [dbo].[GON_SEPARATED_DETAIL] CHECK CONSTRAINT [FK_GON_SEPARATED_DETAIL_GON_SEPARATED_HEADER]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GON_SEPARATED_DETAIL_GON_SEPARATED_PO_DETAIL]') AND parent_object_id = OBJECT_ID(N'[dbo].[GON_SEPARATED_DETAIL]'))
ALTER TABLE [dbo].[GON_SEPARATED_DETAIL]  WITH CHECK ADD  CONSTRAINT [FK_GON_SEPARATED_DETAIL_GON_SEPARATED_PO_DETAIL] FOREIGN KEY([FKAppPODetail])
REFERENCES [dbo].[GON_SEPARATED_PO_DETAIL] ([IDApp])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GON_SEPARATED_DETAIL_GON_SEPARATED_PO_DETAIL]') AND parent_object_id = OBJECT_ID(N'[dbo].[GON_SEPARATED_DETAIL]'))
ALTER TABLE [dbo].[GON_SEPARATED_DETAIL] CHECK CONSTRAINT [FK_GON_SEPARATED_DETAIL_GON_SEPARATED_PO_DETAIL]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GON_SEPARATED_HEADER_GON_AREA]') AND parent_object_id = OBJECT_ID(N'[dbo].[GON_SEPARATED_HEADER]'))
ALTER TABLE [dbo].[GON_SEPARATED_HEADER]  WITH CHECK ADD  CONSTRAINT [FK_GON_SEPARATED_HEADER_GON_AREA] FOREIGN KEY([GON_AREA])
REFERENCES [dbo].[GON_AREA] ([GON_ID_AREA])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GON_SEPARATED_HEADER_GON_AREA]') AND parent_object_id = OBJECT_ID(N'[dbo].[GON_SEPARATED_HEADER]'))
ALTER TABLE [dbo].[GON_SEPARATED_HEADER] CHECK CONSTRAINT [FK_GON_SEPARATED_HEADER_GON_AREA]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GON_SEPARATED_HEADER_GON_SEPARATED_HEADER]') AND parent_object_id = OBJECT_ID(N'[dbo].[GON_SEPARATED_HEADER]'))
ALTER TABLE [dbo].[GON_SEPARATED_HEADER]  WITH CHECK ADD  CONSTRAINT [FK_GON_SEPARATED_HEADER_GON_SEPARATED_HEADER] FOREIGN KEY([SHIP_TO])
REFERENCES [dbo].[DIST_DISTRIBUTOR] ([DISTRIBUTOR_ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GON_SEPARATED_HEADER_GON_SEPARATED_HEADER]') AND parent_object_id = OBJECT_ID(N'[dbo].[GON_SEPARATED_HEADER]'))
ALTER TABLE [dbo].[GON_SEPARATED_HEADER] CHECK CONSTRAINT [FK_GON_SEPARATED_HEADER_GON_SEPARATED_HEADER]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GON_SEPARATED_HEADER_GON_TRANSPORTER]') AND parent_object_id = OBJECT_ID(N'[dbo].[GON_SEPARATED_HEADER]'))
ALTER TABLE [dbo].[GON_SEPARATED_HEADER]  WITH CHECK ADD  CONSTRAINT [FK_GON_SEPARATED_HEADER_GON_TRANSPORTER] FOREIGN KEY([TRANSPORTER])
REFERENCES [dbo].[GON_TRANSPORTER] ([GT_ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GON_SEPARATED_HEADER_GON_TRANSPORTER]') AND parent_object_id = OBJECT_ID(N'[dbo].[GON_SEPARATED_HEADER]'))
ALTER TABLE [dbo].[GON_SEPARATED_HEADER] CHECK CONSTRAINT [FK_GON_SEPARATED_HEADER_GON_TRANSPORTER]
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'GON_SEPARATED_HEADER', N'COLUMN',N'SHIP_TO_CUST'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bila Ship_to null maka column ini harus di isi' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GON_SEPARATED_HEADER', @level2type=N'COLUMN',@level2name=N'SHIP_TO_CUST'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'GON_SEPARATED_HEADER', N'COLUMN',N'SHIP_TO_ADDRESS'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Bila Ship_to null maka column ini harus di isi' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GON_SEPARATED_HEADER', @level2type=N'COLUMN',@level2name=N'SHIP_TO_ADDRESS'
GO

IF NOT EXISTS(SELECT FORM_ID FROM SYST_MENU WHERE FORM_NAME='ConvertionProduct')
BEGIN INSERT INTO SYST_MENU(FORM_ID,FORM_NAME,DESCRIPTIONS) 
	VALUES('BRND7','ConvertionProduct','Master data Convertion of product');
END
GO

IF NOT EXISTS(SELECT FORM_ID FROM SYST_MENU WHERE FORM_NAME = 'GonDetailData')
BEGIN INSERT INTO SYST_MENU(FORM_ID,FORM_NAME,DESCRIPTIONS)
VALUES('GUF002','GonDetailData','Tracking Gon data');
END
GO

IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.VIEWS WHERE [TABLE_NAME] = 'uv_gon_separated_PO_Pending_Gon')
DROP VIEW uv_gon_separated_PO_Pending_Gon
GO
CREATE VIEW uv_gon_separated_PO_Pending_Gon
AS
SELECT ISNULL(GON.IDApp,0)AS IDApp,GSPD.IDApp AS IDAppPODetail,GSPH.PO_NUMBER,GSPH.SPPB_NUMBER,GSPH.PO_DATE,GSPH.SPPB_DATE,BP.BRANDPACK_NAME AS ITEM,GSPD.QUANTITY AS PO_ORIGINAL,GSPD.STATUS,ISNULL(GON.GON_NUMBER,'PENDING GON')AS GON_NUMBER,
SHIP_TO_CUSTOMER,GON.GON_DATE,
GON.WARHOUSE,
GON.TRANSPORTER_NAME, GON.POLICE_NO_TRANS,GON.DRIVER_TRANS,GON.QUANTITY,GON.QUANTITY_UNIT,GON.COLLY_BOX,GON.COLLY_PACKSIZE,GON.BATCH_NO,
GON.CreatedBy,GON.CreatedDate,GON.ModifiedBy,GON.ModifiedDate 
FROM GON_SEPARATED_PO_HEADER GSPH INNER JOIN GON_SEPARATED_PO_DETAIL GSPD ON GSPD.FKApp = GSPH.IDApp
INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = GSPD.BRANDPACK_ID
LEFT OUTER JOIN(
SELECT GSD.IDApp,GSH.FKApp,ISNULL(DR.DISTRIBUTOR_NAME,GSH.SHIP_TO_CUST) AS SHIP_TO_CUSTOMER,GSH.GON_NUMBER, 
GSH.GON_DATE,CASE GSH.WARHOUSE WHEN 'SRG' THEN 'SERANG' WHEN 'SBY' THEN 'SURABAYA' WHEN 'JKT' THEN 'JAKARTA' WHEN 'MRK' THEN 'MERAK' WHEN 'TGR' THEN 'TANGERANG' ELSE 'UNKNOWN' END AS WARHOUSE,
GT.TRANSPORTER_NAME, GSH.POLICE_NO_TRANS,GSH.DRIVER_TRANS,GSD.QTY AS QUANTITY,CONVERT(VARCHAR(20),GSD.QTY) + ' ' + BP.UNIT AS QUANTITY_UNIT,GSD.COLLY_BOX,GSD.COLLY_PACKSIZE,ISNULL(GSD.BATCH_NO,'') AS BATCH_NO,
GSD.CreatedBy,GSD.CreatedDate,GSD.ModifiedBy,GSD.ModifiedDate
FROM GON_SEPARATED_DETAIL GSD INNER JOIN GON_SEPARATED_HEADER GSH ON GSH.IDApp = GSD.FKAppGonHeader
INNER JOIN GON_TRANSPORTER GT ON GT.GT_ID = GSH.TRANSPORTER 
INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = GSD.ITEM 
LEFT OUTER JOIN DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = GSH.SHIP_TO 
)GON ON GON.FKApp = GSPD.IDApp
AND GON.IDApp = GSPD.IDApp 
AND GON.FKApp = GSPH.IDApp
WHERE GSPD.STATUS='Pending' AND GON.GON_NUMBER IS NULL
GO

IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.VIEWS WHERE [TABLE_NAME] = 'uv_gon_separated_PO_Status_Completed')
DROP VIEW uv_gon_separated_PO_Status_Completed
GO
CREATE VIEW uv_gon_separated_PO_Status_Completed
AS
SELECT ISNULL(GSD.IDApp,0)AS IDApp,GSPD.IDApp AS IDAppPODetail,GSPH.PO_NUMBER,GSPH.SPPB_NUMBER,GSPH.PO_DATE,GSPH.SPPB_DATE,BP.BRANDPACK_NAME AS ITEM,GSPD.QUANTITY AS PO_ORIGINAL,GSPD.STATUS,ISNULL(GSH.GON_NUMBER,'PENDING GON')AS GON_NUMBER,ISNULL(DR.DISTRIBUTOR_NAME,GSH.SHIP_TO_CUST) AS SHIP_TO_CUSTOMER,GSH.GON_DATE,
CASE GSH.WARHOUSE WHEN 'SRG' THEN 'SERANG' WHEN 'SBY' THEN 'SURABAYA' WHEN 'JKT' THEN 'JAKARTA' WHEN 'MRK' THEN 'MERAK' WHEN 'TGR' THEN 'TANGERANG' ELSE 'UNKNOWN' END AS WARHOUSE,
GT.TRANSPORTER_NAME, GSH.POLICE_NO_TRANS,GSH.DRIVER_TRANS,GSD.QTY AS QUANTITY,CONVERT(VARCHAR(20),GSD.QTY) + ' ' + BP.UNIT AS QUANTITY_UNIT,GSD.COLLY_BOX,GSD.COLLY_PACKSIZE,ISNULL(GSD.BATCH_NO,'') AS BATCH_NO,
GSD.CreatedBy,GSD.CreatedDate,GSD.ModifiedBy,GSD.ModifiedDate 
FROM GON_SEPARATED_PO_HEADER GSPH INNER JOIN GON_SEPARATED_PO_DETAIL GSPD ON GSPD.FKApp = GSPH.IDApp
INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = GSPD.BRANDPACK_ID
LEFT OUTER JOIN GON_SEPARATED_HEADER GSH ON GSH.FKApp = GSPH.IDApp
LEFT OUTER JOIN DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = GSH.SHIP_TO 
LEFT OUTER JOIN GON_TRANSPORTER GT ON GT.GT_ID = GSH.TRANSPORTER LEFT OUTER JOIN GON_SEPARATED_DETAIL GSD ON GSH.IDApp = GSD.FKAppGonHeader
AND GSD.FKAppPODetail = GSPD.IDApp 
AND GSD.ITEM = BP.BRANDPACK_ID 
WHERE GSPD.STATUS='Completed'
GO

IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.VIEWS WHERE [TABLE_NAME] = 'uv_gon_separated_PO_All')
DROP VIEW uv_gon_separated_PO_All
GO
CREATE VIEW uv_gon_separated_PO_All
AS
SELECT ISNULL(GSD.IDApp,0)AS IDApp,GSPD.IDApp AS IDAppPODetail,GSPH.PO_NUMBER,GSPH.SPPB_NUMBER,GSPH.PO_DATE,GSPH.SPPB_DATE,BP.BRANDPACK_NAME AS ITEM,GSPD.QUANTITY AS PO_ORIGINAL,GSPD.STATUS,
GON_NUMBER = CASE WHEN GSPD.STATUS = 'PENDING' THEN 'PENDING GON'
WHEN GSH.GON_NUMBER IS NULL THEN 'PENDING GON'
WHEN GSH.GON_NUMBER IS NOT NULL THEN GSH.GON_NUMBER END,
ISNULL(DR.DISTRIBUTOR_NAME,GSH.SHIP_TO_CUST) AS SHIP_TO_CUSTOMER,GSH.GON_DATE,
CASE GSH.WARHOUSE WHEN 'SRG' THEN 'SERANG' WHEN 'SBY' THEN 'SURABAYA' WHEN 'JKT' THEN 'JAKARTA' WHEN 'MRK' THEN 'MERAK' WHEN 'TGR' THEN 'TANGERANG' ELSE 'UNKNOWN' END AS WARHOUSE,
GT.TRANSPORTER_NAME, GSH.POLICE_NO_TRANS,GSH.DRIVER_TRANS,GSD.QTY AS QUANTITY,CONVERT(VARCHAR(20),GSD.QTY) + ' ' + BP.UNIT AS QUANTITY_UNIT,GSD.COLLY_BOX,GSD.COLLY_PACKSIZE,ISNULL(GSD.BATCH_NO,'') AS BATCH_NO,
GSD.CreatedBy,GSD.CreatedDate,GSD.ModifiedBy,GSD.ModifiedDate 
FROM GON_SEPARATED_PO_HEADER GSPH INNER JOIN GON_SEPARATED_PO_DETAIL GSPD ON GSPD.FKApp = GSPH.IDApp
INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = GSPD.BRANDPACK_ID
LEFT OUTER JOIN GON_SEPARATED_HEADER GSH ON GSH.FKApp = GSPH.IDApp
LEFT OUTER JOIN DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = GSH.SHIP_TO 
LEFT OUTER JOIN GON_TRANSPORTER GT ON GT.GT_ID = GSH.TRANSPORTER LEFT OUTER JOIN GON_SEPARATED_DETAIL GSD ON GSH.IDApp = GSD.FKAppGonHeader
AND GSD.FKAppPODetail = GSPD.IDApp 
AND GSD.ITEM = BP.BRANDPACK_ID 
GO
--select * from uv_gon_separated_PO_All
--SET NOCOUNT ON; SELECT TOP 500 * 
-- FROM uv_gon_separated_PO_All 
-- WHERE IDApp > ALL(SELECT TOP 0 IDApp  FROM uv_gon_separated_PO_All WHERE (GON_NUMBER LIKE '%%') ORDER BY IDApp DESC) AND GON_NUMBER LIKE '%%' ORDER BY IDApp DESC 


IF EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.VIEWS WHERE [TABLE_NAME] = 'uv_gon_separated_PO')
DROP VIEW uv_gon_separated_PO
GO
CREATE VIEW uv_gon_separated_PO
AS
SELECT GSD.IDApp,GSPD.IDApp AS IDAppPODetail,GSPH.PO_NUMBER,GSPH.SPPB_NUMBER,GSPH.PO_DATE,GSPH.SPPB_DATE,BP.BRANDPACK_NAME AS ITEM,GSPD.QUANTITY AS PO_ORIGINAL,GSPD.STATUS,ISNULL(GSH.GON_NUMBER,'PENDING GON')AS GON_NUMBER,ISNULL(DR.DISTRIBUTOR_NAME,GSH.SHIP_TO_CUST) AS SHIP_TO_CUSTOMER,GSH.GON_DATE,
CASE GSH.WARHOUSE WHEN 'SRG' THEN 'SERANG' WHEN 'SBY' THEN 'SURABAYA' WHEN 'JKT' THEN 'JAKARTA' WHEN 'MRK' THEN 'MERAK' WHEN 'TGR' THEN 'TANGERANG' ELSE 'UNKNOWN' END AS WARHOUSE,
GT.TRANSPORTER_NAME, GSH.POLICE_NO_TRANS,GSH.DRIVER_TRANS,GSD.QTY AS QUANTITY,CONVERT(VARCHAR(20),GSD.QTY) + ' ' + BP.UNIT AS QUANTITY_UNIT,GSD.COLLY_BOX,GSD.COLLY_PACKSIZE,ISNULL(GSD.BATCH_NO,'') AS BATCH_NO,
GSD.CreatedBy,GSD.CreatedDate,GSD.ModifiedBy,GSD.ModifiedDate 
FROM GON_SEPARATED_PO_HEADER GSPH INNER JOIN GON_SEPARATED_PO_DETAIL GSPD ON GSPD.FKApp = GSPH.IDApp
INNER JOIN BRND_BRANDPACK BP ON BP.BRANDPACK_ID = GSPD.BRANDPACK_ID
INNER JOIN GON_SEPARATED_DETAIL GSD ON GSD.FKAppPODetail = GSPD.IDApp 
INNER JOIN GON_SEPARATED_HEADER GSH ON GSH.FKApp = GSPH.IDApp
LEFT OUTER JOIN DIST_DISTRIBUTOR DR ON DR.DISTRIBUTOR_ID = GSH.SHIP_TO 
INNER JOIN GON_TRANSPORTER GT ON GT.GT_ID = GSH.TRANSPORTER 
AND GSH.IDApp = gsd.FKAppGonHeader  
--AND GSD.ITEM = BP.BRANDPACK_ID 
GO