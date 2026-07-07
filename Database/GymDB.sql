-- GymManagement Database Setup Script
-- Run this script in SQL Server Management Studio

CREATE DATABASE GymManagementDB;
GO

USE GymManagementDB;
GO

-- Members Table
CREATE TABLE Members (
    MemberID INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    DateOfBirth DATE,
    Gender VARCHAR(10),
    PhoneNumber VARCHAR(15),
    Address VARCHAR(200),
    TrainingProgram VARCHAR(100),
    MembershipStartDate DATE,
    MembershipEndDate DATE
);
GO

-- Classes Table
CREATE TABLE Classes (
    ClassID INT PRIMARY KEY IDENTITY(1,1),
    ClassName VARCHAR(100) NOT NULL,
    Description VARCHAR(300),
    Instructor VARCHAR(100),
    Schedule VARCHAR(100),
    Capacity INT,
    Duration VARCHAR(50)
);
GO

-- Stored Procedures

-- Get All Members
CREATE PROCEDURE sp_GetAllMembers
AS
BEGIN
    SELECT * FROM Members
END
GO

-- Add Member
CREATE PROCEDURE sp_AddMember
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @DateOfBirth DATE,
    @Gender VARCHAR(10),
    @PhoneNumber VARCHAR(15),
    @Address VARCHAR(200),
    @TrainingProgram VARCHAR(100),
    @MembershipStartDate DATE,
    @MembershipEndDate DATE
AS
BEGIN
    INSERT INTO Members (FirstName, LastName, DateOfBirth, Gender, PhoneNumber, Address, TrainingProgram, MembershipStartDate, MembershipEndDate)
    VALUES (@FirstName, @LastName, @DateOfBirth, @Gender, @PhoneNumber, @Address, @TrainingProgram, @MembershipStartDate, @MembershipEndDate)
END
GO

-- Update Member
CREATE PROCEDURE sp_UpdateMember
    @MemberID INT,
    @FirstName VARCHAR(50),
    @LastName VARCHAR(50),
    @DateOfBirth DATE,
    @Gender VARCHAR(10),
    @PhoneNumber VARCHAR(15),
    @Address VARCHAR(200),
    @TrainingProgram VARCHAR(100),
    @MembershipStartDate DATE,
    @MembershipEndDate DATE
AS
BEGIN
    UPDATE Members SET
        FirstName = @FirstName,
        LastName = @LastName,
        DateOfBirth = @DateOfBirth,
        Gender = @Gender,
        PhoneNumber = @PhoneNumber,
        Address = @Address,
        TrainingProgram = @TrainingProgram,
        MembershipStartDate = @MembershipStartDate,
        MembershipEndDate = @MembershipEndDate
    WHERE MemberID = @MemberID
END
GO

-- Delete Member
CREATE PROCEDURE sp_DeleteMember
    @MemberID INT
AS
BEGIN
    DELETE FROM Members WHERE MemberID = @MemberID
END
GO

-- Search Member by ID or Name
CREATE PROCEDURE sp_SearchMember
    @SearchTerm VARCHAR(100)
AS
BEGIN
    SELECT * FROM Members
    WHERE CAST(MemberID AS VARCHAR) LIKE '%' + @SearchTerm + '%'
    OR FirstName LIKE '%' + @SearchTerm + '%'
    OR LastName LIKE '%' + @SearchTerm + '%'
END
GO

-- Search Members by Instructor (through TrainingProgram)
CREATE PROCEDURE sp_GetMembersByInstructor
    @Instructor VARCHAR(100)
AS
BEGIN
    SELECT m.* FROM Members m
    INNER JOIN Classes c ON m.TrainingProgram = c.ClassName
    WHERE c.Instructor LIKE '%' + @Instructor + '%'
END
GO

-- Get All Classes
CREATE PROCEDURE sp_GetAllClasses
AS
BEGIN
    SELECT * FROM Classes
END
GO

-- Add Class
CREATE PROCEDURE sp_AddClass
    @ClassName VARCHAR(100),
    @Description VARCHAR(300),
    @Instructor VARCHAR(100),
    @Schedule VARCHAR(100),
    @Capacity INT,
    @Duration VARCHAR(50)
AS
BEGIN
    INSERT INTO Classes (ClassName, Description, Instructor, Schedule, Capacity, Duration)
    VALUES (@ClassName, @Description, @Instructor, @Schedule, @Capacity, @Duration)
END
GO

-- Update Class
CREATE PROCEDURE sp_UpdateClass
    @ClassID INT,
    @ClassName VARCHAR(100),
    @Description VARCHAR(300),
    @Instructor VARCHAR(100),
    @Schedule VARCHAR(100),
    @Capacity INT,
    @Duration VARCHAR(50)
AS
BEGIN
    UPDATE Classes SET
        ClassName = @ClassName,
        Description = @Description,
        Instructor = @Instructor,
        Schedule = @Schedule,
        Capacity = @Capacity,
        Duration = @Duration
    WHERE ClassID = @ClassID
END
GO

-- Delete Class
CREATE PROCEDURE sp_DeleteClass
    @ClassID INT
AS
BEGIN
    DELETE FROM Classes WHERE ClassID = @ClassID
END
GO

-- Search Class
CREATE PROCEDURE sp_SearchClass
    @SearchTerm VARCHAR(100)
AS
BEGIN
    SELECT * FROM Classes
    WHERE ClassName LIKE '%' + @SearchTerm + '%'
    OR Instructor LIKE '%' + @SearchTerm + '%'
END
GO
