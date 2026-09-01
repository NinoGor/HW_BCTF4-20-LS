CREATE DATABASE University;
GO

USE University;
GO

--გაკვეთილზე შექმნილი ცხრილები

CREATE TABLE FACULTY
(
    DepartmentID INT PRIMARY KEY IDENTITY(1,1),
    DepartmentName NVARCHAR(100) NOT NULL
);

CREATE TABLE STUDENTS
(
    ID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL, 
    Email VARCHAR(255) NOT NULL UNIQUE,
    Age INT NOT NULL CHECK(Age BETWEEN 18 AND 100), 
    GPA DECIMAL(3,2) CHECK(GPA BETWEEN 0.00 AND 4.00), 
    IsActive BIT DEFAULT(1),
    RegisteredAt DATETIME2 DEFAULT(SYSDATETIME()),
    PhoneNumber VARCHAR(20) NULL,
    DepartmentID INT,
    CONSTRAINT FK_STUDENTS_FACULTY FOREIGN KEY (DepartmentID) REFERENCES FACULTY(DepartmentID)
);


--1. One-to-One (1:1) – სტუდენტის პროფილი (STUDENT_DETAILS)
-- შექმენით ცხრილი STUDENT_DETAILS, სადაც შეინახავთ სტუდენტის დამატებით პერსონალურ ინფორმაციას: Address (NVARCHAR), PassportNumber (VARCHAR, UNIQUE, NOT NULL), DateOfBirth (DATE).

CREATE TABLE STUDENT_DETAILS
(
    StudentID INT PRIMARY KEY,
    Address NVARCHAR(255),
    PassportNumber VARCHAR(50) UNIQUE NOT NULL,
    DateOfBirth DATE,
    -- დააკავშირეთ STUDENTS ცხრილთან 1:1 მიმართებით (StudentID უნდა იყოს STUDENT_DETAILS-ის Primary Key და ამავდროულად Foreign Key, რომელიც მიუთითებს STUDENTS(ID)-ზე).
    CONSTRAINT FK_STUDENT_DETAILS_STUDENTS FOREIGN KEY (StudentID) REFERENCES STUDENTS(ID)
);
GO


--2. One-to-Many (1:M) – ლექტორები და კურსები (INSTRUCTORS & COURSES)
--შექმენით ცხრილი INSTRUCTORS: InstructorID (PK, IDENTITY), FirstName (NVARCHAR), LastName (NVARCHAR), Email (UNIQUE).
CREATE TABLE INSTRUCTORS
(
    InstructorID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email VARCHAR(255) UNIQUE NOT NULL
);
GO

--შექმენით ცხრილი COURSES: CourseID (PK, IDENTITY), CourseTitle (NVARCHAR, NOT NULL), Credits (INT, CHECK 1-დან 6-მდე), InstructorID (FK).
CREATE TABLE COURSES
(
    CourseID INT PRIMARY KEY IDENTITY(1,1),
    CourseTitle NVARCHAR(100) NOT NULL,
    Credits INT CHECK (Credits BETWEEN 1 AND 6),
    InstructorID INT,
    --თითოეულ კურსს ჰყავს 1 ლექტორი, ხოლო ლექტორს შეძლია გაუძღვეს რამდენიმე კურსს (1:M).
    CONSTRAINT FK_COURSES_INSTRUCTORS FOREIGN KEY (InstructorID) REFERENCES INSTRUCTORS(InstructorID)
);
GO

--3. Many-to-Many (M:N) – კურსზე რეგისტრაცია (ENROLLMENTS)
--შექმენით შუალედური (Junction) ცხრილი ENROLLMENTS, რომელიც დააკავშირებს STUDENTS და COURSES ცხრილებს (M:N).
--სვეტები: EnrollmentID (PK, IDENTITY), StudentID (FK), CourseID (FK), EnrollmentDate (DATETIME2, DEFAULT SYSDATETIME()), Grade (DECIMAL(3,2), CHECK 0.00-დან 4.00-მდე).
CREATE TABLE ENROLLMENTS
(
    EnrollmentID INT PRIMARY KEY IDENTITY(1,1),
    StudentID INT,
    CourseID INT,
    EnrollmentDate DATETIME2 DEFAULT SYSDATETIME(),
    Grade DECIMAL(3,2) CHECK (Grade BETWEEN 0.00 AND 4.00),
    CONSTRAINT FK_ENROLLMENTS_STUDENTS FOREIGN KEY (StudentID) REFERENCES STUDENTS(ID),
    CONSTRAINT FK_ENROLLMENTS_COURSES FOREIGN KEY (CourseID) REFERENCES COURSES(CourseID),
    
    --დაამატეთ UNIQUE შეზღუდვა წყვილზე (StudentID, CourseID), რათა სტუდენტი ერთსა და იმავე კურსზე ორჯერ ვერ დარეგისტრირდეს.
    CONSTRAINT UQ_Student_Course UNIQUE (StudentID, CourseID) 
);
GO






--ნაწილი 2: მონაცემების შევსება (DML)
--================================--
--"Any similarity to actual persons, living or dead, is purely coincidental." :)

--ჩაწერეთ 3 ფაკულტეტი, 
INSERT INTO FACULTY 
(DepartmentName)
VALUES --როგორც გაკვეთილზე
(N'კომპიუტერული მეცნიერება'),
(N'მათემატიკა'),
(N'ფიზიკა'); 

--5 სტუდენტი, 
INSERT INTO STUDENTS 
(FirstName, Email, Age, GPA, PhoneNumber, DepartmentID)
VALUES
(N'გიორგი', 'giorgi@gmail.com', 20, 3.80, '+995555111111', 1),
(N'სალომე', 'salome@gmail.com', 21, 3.95, '+995555222222', 2),
(N'დავითი', 'davit@gmail.com', 22, 2.50, '+995555333333', 1),
(N'მარიამი', 'mariam@gmail.com', 19, 3.23, '+995555444444', 3),
(N'ლუკა', 'luka@gmail.com', 23, 1.80, '+995555555555', 2);

--5 სტუდენტის პროფილი, 
INSERT INTO STUDENT_DETAILS
(StudentID, Address, PassportNumber, DateOfBirth)
VALUES
(1, N'თბილისი, რუსთაველის გამზ.', 'AA1234567', '2004-05-15'),
(2, N'ქუთაისი, წერეთლის ქ.', 'BB2345678', '2003-08-22'),
(3, N'ბათუმი, გორგილაძის ქ.', 'CC3456789', '2002-11-10'),
(4, N'თბილისი, პეკინის გამზ.', 'DD4567890', '2005-02-28'),
(5, N'რუსთავი, შარტავას ქ.', 'EE5678901', '2001-09-05');

--3 ლექტორი, 
INSERT INTO INSTRUCTORS (FirstName, LastName, Email) VALUES
(N'ზურაბ', N'თამარაშვილი', 'zurab.t@university.ge'),
(N'თამარ', N'ზურაბიშვილი', 'tamar.z@university.ge'),
(N'დავით', N'დავითაშვილი', 'davit.d@university.ge');

--4 კურსი 
INSERT INTO COURSES 
(CourseTitle, Credits, InstructorID) 
VALUES
(N'დაპროგრამების საფუძვლები', 5, 1),
(N'მონაცემთა ბაზები', 6, 1),
(N'წრფივი ალგებრა', 5, 2),
(N'მექანიკა', 4, 3);

--და მინიმუმ 8 რეგისტრაცია (ENROLLMENTS-ში). 
INSERT INTO ENROLLMENTS 
(StudentID, CourseID, Grade) 
VALUES --ბოლო სტუდენტს არ დავარეგისტრირებ 3.7 სავარჯიშოსთვის
(1, 1, 3.50),
(1, 2, 4.00),
(2, 3, 3.80),
(2, 1, 3.90),
(3, 1, 2.00),
(3, 2, 2.50),
(4, 4, 3.10),
(4, 3, 3.40);
GO

--ნაწილი 3: SELECT სავარჯიშოები
--============================--

--1. გამოიტანეთ ყველა სტუდენტის FirstName, Email, და მათი ფაკულტეტის დასახელება (DepartmentName).
SELECT 
    S.FirstName, 
    S.Email,
    F.DepartmentName
FROM STUDENTS S
JOIN FACULTY F ON S.DepartmentID = F.DepartmentID;


--2. გამოიტანეთ კურსების სია (CourseTitle, Credits) მათ პასუხისმგებელ ლექტორებთან ერთად (FirstName, LastName).
SELECT 
    C.CourseTitle, 
    C.Credits, 
    I.FirstName + ' ' + I.LastName AS Instructor
FROM COURSES C
JOIN INSTRUCTORS I ON C.InstructorID = I.InstructorID;


--3. გამოიტანეთ იმ სტუდენტების სია (FirstName, PassportNumber), რომელთა GPA აღემატება 3.00-ს.
SELECT 
    S.FirstName, 
    SD.PassportNumber
    --,S.GPA
FROM STUDENTS S
JOIN STUDENT_DETAILS SD ON S.ID = SD.StudentID
WHERE S.GPA > 3.00;

--4. გამოიტანეთ სტუდენტის სახელი (FirstName), კურსის დასახელება (CourseTitle) და მიღებული ქულა (Grade) ყველა რეგისტრაციისთვის.
SELECT 
    S.FirstName, 
    C.CourseTitle, 
    E.Grade
FROM ENROLLMENTS E
JOIN STUDENTS S ON E.StudentID = S.ID
JOIN COURSES C ON E.CourseID = C.CourseID;

--5. იპოვეთ თითოეული სტუდენტის საშუალო ქულა (AVG(Grade)), რომელიც მიიღო კურსებში. გამოიტანეთ სტუდენტის სახელი და საშუალო ქულა.
SELECT 
    S.FirstName, 
    AVG(E.Grade) AS AverageGrade
FROM STUDENTS S
JOIN ENROLLMENTS E ON S.ID = E.StudentID
GROUP BY S.FirstName, S.ID;


--6. დაითვალეთ, რამდენი სტუდენტია დარეგისტრირებული თითოეულ კურსზე (CourseTitle, StudentCount).
SELECT 
    C.CourseTitle, 
    COUNT(E.StudentID) AS StudentCount
FROM COURSES C
LEFT JOIN ENROLLMENTS E ON C.CourseID = E.CourseID
GROUP BY C.CourseTitle;

--7. გამოიტანეთ იმ სტუდენტების სია, რომლებსაც ჯერ არცერთ კურსზე არ გაუვლიათ რეგისტრაცია (LEFT JOIN / IS NULL).
SELECT 
    S.*
FROM STUDENTS S
LEFT JOIN ENROLLMENTS E ON S.ID = E.StudentID
WHERE E.EnrollmentID IS NULL;

--8. იპოვეთ ყველაზე მაღალი GPA-ს მქონე სტუდენტის მიერ არჩეული კურსების დასახელებები.
SELECT 
    C.CourseTitle
FROM STUDENTS S
JOIN ENROLLMENTS E ON S.ID = E.StudentID
JOIN COURSES C ON E.CourseID = C.CourseID
WHERE S.GPA = (SELECT MAX(GPA) FROM STUDENTS);
