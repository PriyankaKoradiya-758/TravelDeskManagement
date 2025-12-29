USE [TravelDeskDB]
GO

INSERT INTO [dbo].[AdminUsers]
           ([Username]
           ,[PasswordHash]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[IsDeleted])
     VALUES
           ('Admin'
           ,'$2a$11$7RjNQ9O9pnbLRGcEjmvMzerrrRWI/uHVuIUWtjRR4pZdHB/xmz83W'
           ,-1
           ,GETUTCDATE()
           ,0)
GO