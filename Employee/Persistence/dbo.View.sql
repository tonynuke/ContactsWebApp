CREATE VIEW [dbo].[PlainEmployeeLinks]
	AS SELECT
		NEWID() as Id,
		Organisations.Id as OrganisationId,		
		Organisations.Name as OrganisationName,
		Employee.Id as EmployeeId,
		Employee.Name as EmployeeName,
		Employee.Surname,
		Employee.Patronymic,
		Employee.Position,
		Employee.BirthDate,
		Link.Id as LinkId,
		Link.Type as LinkType,
		Link.Value as LinkValue
	FROM Employee
	LEFT JOIN Organisations ON Employee.OrganisationId = Organisations.Id
	LEFT JOIN Link ON Employee.Id = Link.EmployeeId