GO
INSERT INTO Companies (Name, Location, Industry) VALUES
('SoftServe', 'Lviv', 'IT'),
('EPAM', 'Kyiv', 'IT'),
('GlobalLogic', 'Kharkiv', 'IT');
select * From Companies
GO

GO
INSERT INTO UserBases ([Name], Email, PasswordHash, [Role], CompanyId) VALUES
('Admin1', 'admin1@mail.com', 'hash', 2, NULL),
('Admin2', 'admin2@mail.com', 'hash', 2, NULL),
('Admin3', 'admin3@mail.com', 'hash', 2, NULL),

('HR1', 'hr1@mail.com', 'hash', 1, 1),
('HR2', 'hr2@mail.com', 'hash', 1, 1),
('HR3', 'hr3@mail.com', 'hash', 1, 2),
('HR4', 'hr4@mail.com', 'hash', 1, 2),
('HR5', 'hr5@mail.com', 'hash', 1, 3),
('HR6', 'hr6@mail.com', 'hash', 1, 3),

('Emp1', 'emp1@mail.com', 'hash', 0, null),
('Emp2', 'emp2@mail.com', 'hash', 0, NULL),
('Emp3', 'emp3@mail.com', 'hash', 0,NULL),
('Emp4', 'emp4@mail.com', 'hash', 0, NULL),
('Emp5', 'emp5@mail.com', 'hash', 0, NULL),
('Emp6', 'emp6@mail.com', 'hash', 0, NULL),
('Emp7', 'emp7@mail.com', 'hash', 0, NULL),
('Emp8', 'emp8@mail.com', 'hash', 0, NULL),
('Emp9', 'emp9@mail.com', 'hash', 0, NULL),
('Emp10', 'emp10@mail.com', 'hash', 0, NULL),
('Emp11', 'emp11@mail.com', 'hash', 0, NULL);
select * From UserBases
GO



GO
INSERT INTO Vacancies ( Title, Description, Salary, PostedDate, IsActive, CompanyId) VALUES
( 'Backend Dev', 'C# .NET', 2000, GETDATE(), 1, 1),
( 'Frontend Dev', 'React', 1800, GETDATE(), 1, 1),
( 'QA Engineer', 'Manual QA', 1500, GETDATE(), 1, 1),
( 'DevOps', 'AWS', 2500, GETDATE(), 1, 2),
( 'Backend Dev', 'Java', 2200, GETDATE(), 1, 2),
( 'Frontend Dev', 'Angular', 1700, GETDATE(), 1, 2),
( 'QA Automation', 'Selenium', 2000, GETDATE(), 1, 2),
( 'Project Manager', 'Scrum', 2300, GETDATE(), 1, 3),
( 'Business Analyst', 'Docs', 1600, GETDATE(), 1, 3),
('Backend Dev', 'Node.js', 2100, GETDATE(), 1, 3),
('Frontend Dev', 'Vue', 1750, GETDATE(), 1, 3),
( 'QA Engineer', 'API testing', 1400, GETDATE(), 1, 1),
( 'DevOps', 'Azure', 2600, GETDATE(), 1, 2),
( 'Mobile Dev', 'Flutter', 1900, GETDATE(), 1, 3),
('Data Engineer', 'ETL', 2700, GETDATE(), 1, 1);
select * From Vacancies
GO



GO
INSERT INTO Applications ( EmployeeId, VacancyId, ApplicationDate, Status, CvPath) VALUES
 (10, 1, GETDATE(), 0, 'cv1.pdf'),
 (11, 2, GETDATE(), 1, 'cv2.pdf'),
 (12, 3, GETDATE(), 0, 'cv3.pdf'),
 (13, 4, GETDATE(), 2, 'cv4.pdf'),
 (14, 5, GETDATE(), 0, 'cv5.pdf'),
 (15, 6, GETDATE(), 1, 'cv6.pdf'),
 (16, 7, GETDATE(), 0, 'cv7.pdf'),
 (17, 8, GETDATE(), 2, 'cv8.pdf'),
 (18, 9, GETDATE(), 0, 'cv9.pdf'),
( 19, 10, GETDATE(), 1, 'cv10.pdf'),
( 20, 11, GETDATE(), 0, 'cv11.pdf'),
( 10, 12, GETDATE(), 1, 'cv12.pdf'),
( 11, 13, GETDATE(), 0, 'cv13.pdf'),
( 12, 14, GETDATE(), 2, 'cv14.pdf'),
( 13, 15, GETDATE(), 0, 'cv15.pdf'),
( 14, 1, GETDATE(), 1, 'cv16.pdf'),
( 15, 2, GETDATE(), 0, 'cv17.pdf'),
( 16, 3, GETDATE(), 2, 'cv18.pdf'),
( 17, 4, GETDATE(), 0, 'cv19.pdf'),
( 18, 5, GETDATE(), 1, 'cv20.pdf'),
( 19, 6, GETDATE(), 0, 'cv21.pdf'),
( 20, 7, GETDATE(), 2, 'cv22.pdf'),
( 10, 8, GETDATE(), 0, 'cv23.pdf'),
( 11, 9, GETDATE(), 1, 'cv24.pdf'),
( 12, 10, GETDATE(), 0, 'cv25.pdf'),
( 13, 11, GETDATE(), 2, 'cv26.pdf'),
( 14, 12, GETDATE(), 0, 'cv27.pdf'),
( 15, 13, GETDATE(), 1, 'cv28.pdf'),
( 16, 14, GETDATE(), 0, 'cv29.pdf'),
( 17, 15, GETDATE(), 2, 'cv30.pdf');
select * From Applications
GO