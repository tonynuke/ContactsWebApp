CREATE VIEW [dbo].[Employees]
	AS SELECT
		Organisations.Id as OrganisationId,		
		Organisations.Name as OrganisationName,
		Employee.Id as EmployeeId,
		Employee.Name as EmployeeName,
		Employee.Surname,
		Employee.Patronymic,
		Employee.Position,
		Employee.BirthDate
	FROM Employee
	JOIN Organisations ON Employee.OrganisationId = Organisations.Id
