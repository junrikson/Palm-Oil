use POS
go

alter table sales alter column DOCode varchar(255) null
go

CREATE TABLE [dbo].[masterMill](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[code] [varchar](255) NOT NULL,
	[name] [varchar](255) NOT NULL,
	[address] [varchar](255) NULL,
	[phone] [varchar](255) NULL,
	[handphone] [varchar](255) NULL,
	[fax] [varchar](255) NULL,
	[email] [varchar](255) NULL,
	[note] [varchar](255) NULL,
	[status] [smallint] NOT NULL CONSTRAINT [DF__masterMi__statu__41248F15]  DEFAULT ((1)),
	[username] [varchar](255) NULL,
	[updated] [datetime] NULL,
	[created] [datetime] NULL CONSTRAINT [DF__masterMi__times__4218B34E]  DEFAULT (getdate()),
 CONSTRAINT [PK__masterMi__3214EC279536E23C] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_masterMill] UNIQUE NONCLUSTERED 
(
	[code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

INSERT INTO [dbo].[masterMenu]
           ([code]
           ,[name]
           ,[status]
           ,[username]
           ,[updated]
           ,[created])
     VALUES
           ('A0002', 'Master Pabrik', 1, 'A', '2018-09-05','2018-09-05')
GO

alter table sales add millCode varchar(255) null
go

alter table sales add foreign key (millCode) references masterMill(code)
go 