USE UNIVERSITY
GO

-- პროცედურები repository მეთოდებისთვის, პროცედურის გამოყენების ვერსიების გასატესტად

------------------------
-- STUDENT პროცედურები
------------------------

-- INSERT
CREATE PROCEDURE sp_InsertStudent
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Email VARCHAR(255),
    @Age INT,
    @GPA DECIMAL(3,2),
    @PhoneNumber VARCHAR(20),
    @IsActive BIT,
    @RegisteredAt DATETIME2,
    @DepartmentID INT
AS
BEGIN
    INSERT INTO STUDENTS (FirstName, LastName, Email, Age, GPA, PhoneNumber, IsActive, RegisteredAt, DepartmentID)
    VALUES (@FirstName, @LastName, @Email, @Age, @GPA, @PhoneNumber, @IsActive, @RegisteredAt, @DepartmentID)
END
GO

-- UPDATE (სრული, არა მარტო GPA)
CREATE PROCEDURE sp_UpdateStudent
    @Id INT,
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Email VARCHAR(255),
    @Age INT,
    @GPA DECIMAL(3,2),
    @PhoneNumber VARCHAR(20),
    @IsActive BIT,
    @DepartmentID INT
AS
BEGIN
    UPDATE STUDENTS
    SET FirstName = @FirstName,
        LastName = @LastName,
        Email = @Email,
        Age = @Age,
        GPA = @GPA,
        PhoneNumber = @PhoneNumber,
        IsActive = @IsActive,
        DepartmentID = @DepartmentID
    WHERE ID = @Id
END
GO

-- DELETE
CREATE PROCEDURE sp_DeleteStudent
    @Id INT
AS
BEGIN
    DELETE FROM STUDENTS WHERE ID = @Id
END
GO

---------------------------
-- INSTRUCTOR პროცედურები
---------------------------

-- GET ALL
CREATE PROCEDURE sp_GetAllInstructors
AS
BEGIN
    SELECT InstructorID, FirstName, LastName, Email
    FROM INSTRUCTORS
END
GO

-- GET BY ID
CREATE PROCEDURE sp_GetInstructorById
    @InstructorID INT
AS
BEGIN
    SELECT InstructorID, FirstName, LastName, Email
    FROM INSTRUCTORS
    WHERE InstructorID = @InstructorID
END
GO

-- INSERT
CREATE PROCEDURE sp_InsertInstructor
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Email VARCHAR(255)
AS
BEGIN
    INSERT INTO INSTRUCTORS (FirstName, LastName, Email)
    VALUES (@FirstName, @LastName, @Email)
END
GO

-- UPDATE
CREATE PROCEDURE sp_UpdateInstructor
    @InstructorID INT,
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @Email VARCHAR(255)
AS
BEGIN
    UPDATE INSTRUCTORS
    SET FirstName = @FirstName,
        LastName = @LastName,
        Email = @Email
    WHERE InstructorID = @InstructorID
END
GO

-- DELETE
CREATE PROCEDURE sp_DeleteInstructor
    @InstructorID INT
AS
BEGIN
    DELETE FROM INSTRUCTORS WHERE InstructorID = @InstructorID
END
GO