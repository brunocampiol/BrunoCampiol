﻿CREATE TABLE [dbo].[VISITORS]
(
	[IP] NVARCHAR(45) NOT NULL PRIMARY KEY,
	[COUNTRY] NVARCHAR(2) NOT NULL,
	[REGION] NVARCHAR(MAX) NULL,
	[CITY] NVARCHAR(MAX) NULL,
	[ISP] NVARCHAR(MAX) NULL,
	[CLIENT_HEADERS] NVARCHAR(MAX),
	[CLIENT_USER_AGENT] NVARCHAR(MAX),
	[CLIENT_BROWSER] NVARCHAR(MAX),
	[CLIENT_OS] NVARCHAR(MAX),
	[CREATED_ON_UTC] DATETIME NOT NULL
)
