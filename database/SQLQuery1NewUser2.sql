USE [Phone]
GO
/****** Object:  StoredProcedure [dbo].[AddUser]    Script Date: 02/04/2016 18:35:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[AddUser]
    @pLogin NVARCHAR(50), 
    @pPassword NVARCHAR(50),
    @pFirstName NVARCHAR(40) = NULL, 
    @pLastName NVARCHAR(40) = NULL,
    @pMobile NVARCHAR(20) = NULL,
    @pEmail NVARCHAR(40) = NULL,
    @pPermissions NVARCHAR(1) = NULL
AS
BEGIN
    SET NOCOUNT ON

    DECLARE @Salt UNIQUEIDENTIFIER=NEWID()
   

        INSERT INTO dbo.[User] (LoginName, PasswordHash, Salt, FirstName, LastName, Mobile, Email, Permissions)
        VALUES(@pLogin, HASHBYTES('SHA1', @pPassword+CAST(@Salt AS NVARCHAR(36))), @Salt, @pFirstName, @pLastName, @pMobile, @pEmail, @pPermissions)
  END
