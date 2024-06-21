-- Crear la base de datos
CREATE DATABASE PRUEBACORTA2;
GO

-- Usar la base de datos
USE PRUEBACORTA2;
GO

-- Crear la tabla Users
CREATE TABLE Users(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserName VARCHAR(200) NOT NULL UNIQUE,
    UserPassword VARCHAR(150) NOT NULL
);
GO

-- Crear la tabla Questions
CREATE TABLE Questions(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    QuestionText VARCHAR(250) NOT NULL,
    CreateDate DATE NOT NULL,
    Estatus INT NOT NULL,
    UserId INT NOT NULL,
    FOREIGN KEY(UserId) REFERENCES Users(Id)
);
GO

-- Crear la tabla Answers
CREATE TABLE Answers(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    AnswerText VARCHAR(250) NOT NULL,
    CreateDate DATE NOT NULL,
    UserId INT NOT NULL,
    QuestionId INT NOT NULL,
    FOREIGN KEY(UserId) REFERENCES Users(Id),
    FOREIGN KEY(QuestionId) REFERENCES Questions(Id)
);
GO

-- Procedimiento almacenado CheckUserNameExists
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
CREATE PROCEDURE [dbo].[CheckUserNameExists]
    @UserName NVARCHAR(200)
AS
BEGIN
    SELECT COUNT(1) AS UserCount
    FROM Users
    WHERE UserName = @UserName;
END;
GO

-- Procedimiento almacenado CreateQuestion
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
CREATE PROCEDURE [dbo].[CreateQuestion]
    @QuestionText VARCHAR(250),
    @CreateDate DATE,
    @Estatus BIT,
    @UserId INT
AS
BEGIN
    INSERT INTO Questions (QuestionText, CreateDate, Estatus, UserId)
    VALUES (@QuestionText, @CreateDate, @Estatus, @UserId);
END;
GO

-- Procedimiento almacenado CreateUser
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
CREATE PROCEDURE [dbo].[CreateUser]
    @UserName NVARCHAR(200),
    @UserPassword NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Users WHERE UserName = @UserName)
    BEGIN
        SELECT 'Username already exists' AS Message;
        RETURN;
    END

    INSERT INTO Users (UserName, UserPassword)
    VALUES (@UserName, @UserPassword);

    SELECT 'User created successfully' AS Message;
END;
GO

-- Procedimiento almacenado DeleteQuestionById
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
CREATE PROCEDURE [dbo].[DeleteQuestionById]
    @QuestionId INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;

    BEGIN TRY
        DELETE FROM Answers
        WHERE QuestionId = @QuestionId;

        DELETE FROM Questions
        WHERE Id = @QuestionId;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
    END CATCH;
END;
GO

-- Procedimiento almacenado GetAnswersForQuestionWithUser
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
CREATE PROCEDURE [dbo].[GetAnswersForQuestionWithUser]
    @QuestionId INT
AS
BEGIN
    SELECT
        A.Id,
        A.AnswerText,
        A.CreateDate,
        A.UserId,
        U.UserName AS UserDisplayName, 
        A.QuestionId,
        Q.QuestionText
    FROM
        Answers A
    INNER JOIN
        Users U ON A.UserId = U.Id 
    INNER JOIN
        Questions Q ON A.QuestionId = Q.Id 
    WHERE
        A.QuestionId = @QuestionId
    ORDER BY 
        A.CreateDate DESC;
END;
GO

-- Procedimiento almacenado GetQuestionById
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
CREATE PROCEDURE [dbo].[GetQuestionById]
    @Id INT
AS
BEGIN
    SELECT q.Id, q.QuestionText, q.CreateDate, q.Estatus, q.UserId, u.UserName
    FROM Questions q
    JOIN Users u ON q.UserId = u.Id
    WHERE q.Id = @Id;
END;
GO

-- Procedimiento almacenado InsertAnswer
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
CREATE PROCEDURE [dbo].[InsertAnswer]
    @AnswerText NVARCHAR(250),
    @CreateDate DATE,
    @UserId INT,
    @QuestionId INT
AS
BEGIN
    INSERT INTO Answers (AnswerText, CreateDate, UserId, QuestionId)
    VALUES (@AnswerText, @CreateDate, @UserId, @QuestionId);
END;
GO

-- Procedimiento almacenado SelectAllQuestionsWithUser
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
CREATE PROCEDURE [dbo].[SelectAllQuestionsWithUser]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT q.Id, q.QuestionText, q.CreateDate, q.Estatus, q.UserId, u.UserName
    FROM Questions q
    INNER JOIN Users u ON q.UserId = u.Id
    ORDER BY q.CreateDate DESC;
END;
GO

-- Procedimiento almacenado SelectQuestionsByUserId
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
CREATE PROCEDURE [dbo].[SelectQuestionsByUserId]
    @UserId INT
AS
BEGIN
    SELECT q.Id, q.QuestionText, q.CreateDate, q.Estatus, q.UserId, u.UserName
    FROM Questions q
    INNER JOIN Users u ON q.UserId = u.Id
    WHERE q.UserId = @UserId
    ORDER BY q.CreateDate DESC;
END;
GO

-- Procedimiento almacenado UpdateQuestion
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
CREATE PROCEDURE [dbo].[UpdateQuestion]
    @Id INT,
    @QuestionText NVARCHAR(MAX),
    @Estatus INT,
    @NewCreateDate DATETIME
AS
BEGIN
    UPDATE Questions
    SET QuestionText = @QuestionText,
        Estatus = @Estatus,
        CreateDate = COALESCE(@NewCreateDate, CreateDate) 
    WHERE Id = @Id;
END;
GO

-- Procedimiento almacenado ValidateUserLogin
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO
CREATE PROCEDURE [dbo].[ValidateUserLogin]
    @UserName NVARCHAR(200),
    @UserPassword NVARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, UserName
    FROM Users
    WHERE UserName = @UserName AND UserPassword = @UserPassword;
END;
GO
