CREATE VIEW [dbo].[Employees]
	AS SELECT
		Organizations.Id as OrganizationId,		
		Organizations.Name as OrganizationName,
		Employee.Id as EmployeeId,
		Employee.Name as EmployeeName,
		Employee.Surname,
		Employee.Patronymic,
		Employee.Position,
		Employee.BirthDate
	FROM Employee
	JOIN Organizations ON Employee.OrganizationId = Organizations.Id
