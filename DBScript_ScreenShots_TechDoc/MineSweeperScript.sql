USE [ProjectsSchema]
GO

/****** Object:  Table [dbo].[MineSweeper]    Script Date: 12/11/2020 7:49:20 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MineSweeper](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Input] [varchar](max) NULL,
	[RequestProcessTimes] [int] NOT NULL,
	[AddedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[IPAddress] [nvarchar](11) NULL,
 CONSTRAINT [PK_MineSweeper] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[MineSweeper] ADD  CONSTRAINT [DF_MineSweeper_RequestProcessTimes]  DEFAULT ((0)) FOR [RequestProcessTimes]
GO




-- =========================================================================




USE [ProjectsSchema]
GO

/****** Object:  Trigger [dbo].[ValidateInput]    Script Date: 12/11/2020 7:50:24 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[ValidateInput] ON  [dbo].[MineSweeper] For INSERT , UPDATE 
AS 
BEGIN
    DECLARE @input VARCHAR(MAX);
	DECLARE @input_len int;
	DECLARE @input_end CHAR(3);
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Select @input  = LTRIM(RTRIM(Input)) from inserted;
	SET @input_len = LEN(@input) ;  
	SET @input_end = SUBSTRING(@input, @input_len-2 , 3);
	
	IF @input_end <> '0 0'
    BEGIN
    --RAISERROR ('Input Must End With 0 0',16,1)
	RAISERROR ('dbo.MineSweeper.ValidateInput Trigger : Input should end with 0 0',16,1)
    ROLLBACK TRANSACTION
    END
	
END
GO

ALTER TABLE [dbo].[MineSweeper] ENABLE TRIGGER [ValidateInput]
GO


-- =============================================================
USE [ProjectsSchema]
GO

/****** Object:  StoredProcedure [dbo].[SP_MineSweeper_GetMineSweeperInput]    Script Date: 12/11/2020 7:51:09 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SP_MineSweeper_GetMineSweeperInput](@input_id bigint = null)
AS
BEGIN
  SET NOCOUNT ON;
  EXEC dbo.SP_MineSweeper_InputProcessCounterUpdate @id = @input_id;
  select * from MineSweeper
  where ID = @input_id
  Or    @input_id IS NULL ;
END

 
GO


-- =============================================================
USE [ProjectsSchema]
GO

/****** Object:  StoredProcedure [dbo].[SP_MineSweeper_InputProcessCounterUpdate]    Script Date: 12/11/2020 7:51:34 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_MineSweeper_InputProcessCounterUpdate](@id bigint = null)
AS
BEGIN
   SET NOCOUNT ON;
  BEGIN TRY
	  BEGIN TRANSACTION MineSweeperProcess
			update dbo.MineSweeper 
			set RequestProcessTimes = RequestProcessTimes + 1 
			where ID  = @id
			or    @id IS NULL
	  COMMIT TRANSACTION MineSweeperProcess
  END TRY

  BEGIN CATCH 
  IF (@@TRANCOUNT > 0)
   BEGIN
      ROLLBACK TRANSACTION MineSweeperProcess
      PRINT 'Error detected, all changes reversed'
   END 
    SELECT
        ERROR_NUMBER() AS ErrorNumber,
        ERROR_SEVERITY() AS ErrorSeverity,
        ERROR_STATE() AS ErrorState,
        ERROR_PROCEDURE() AS ErrorProcedure,
        ERROR_LINE() AS ErrorLine,
        ERROR_MESSAGE() AS ErrorMessage
   END CATCH                    
END

 
GO

-- =============================================================
USE [ProjectsSchema]
GO

/****** Object:  UserDefinedFunction [dbo].[FN_MineSweeper_InputCount]    Script Date: 12/11/2020 7:52:16 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[FN_MineSweeper_InputCount]()
RETURNS int -- or whatever length you need
AS
BEGIN

--variable declarations     
DECLARE @counter  int = 0;
DECLARE @CursorCurrentRead  NVarchar(MAX) = '';

--Declare Cursor 
DECLARE GameMine_Cursor CURSOR
FOR SELECT Input FROM  ProjectsSchema.dbo.MineSweeper;

--Open Cursor
OPEN GameMine_Cursor;

--Fetch From Cursor - intial read
FETCH NEXT FROM GameMine_Cursor INTO @CursorCurrentRead
SET @counter = @counter + 1;
SET @CursorCurrentRead = ''

--Iterate and Fetch From Cursor as long as there's data
WHILE @@FETCH_STATUS = 0
    BEGIN
        FETCH NEXT FROM GameMine_Cursor INTO @CursorCurrentRead;
		--process @CursorCurrentRead , we will just increment the input field
		SET @counter = @counter + 1;
        SET @CursorCurrentRead = ''
    END;

CLOSE GameMine_Cursor;

DEALLOCATE GameMine_Cursor; 
 
Return @counter-1; 

END

GO

