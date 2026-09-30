using System;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using CompanyApp;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

using var context = new AppDbContext();

if (!context.Departments.Any())
{
    var it = new Department { Name = "IT" };
    var hr = new Department { Name = "HR" };
    var finance = new Department { Name = "Finance" };

    context.Departments.AddRange(it, hr, finance);
    context.SaveChanges();

    var employees = new[]
    {
        new Employee { Name = "Alex", Salary = 3500, DepartmentId = it.Id, IsActive = true },
        new Employee { Name = "John", Salary = 4200, DepartmentId = it.Id, IsActive = true },
        new Employee { Name = "Elena", Salary = 2800, DepartmentId = it.Id, IsActive = true },
        new Employee { Name = "Maria", Salary = 3100, DepartmentId = hr.Id, IsActive = true },
        new Employee { Name = "David", Salary = 2500, DepartmentId = hr.Id, IsActive = false },
        new Employee { Name = "Anna", Salary = 4000, DepartmentId = finance.Id, IsActive = true },
        new Employee { Name = "Mark", Salary = 3300, DepartmentId = finance.Id, IsActive = true },
        new Employee { Name = "Kate", Salary = 2900, DepartmentId = finance.Id, IsActive = true }
    };

    context.Employees.AddRange(employees);
    context.SaveChanges();

    Console.WriteLine("База данных успешно заполнена!");
}

ShowEmployeeDetails(context);
ShowEmployeesByDept(context, 1);
UpdateSalary(context, 1, 5000);

void ShowEmployeeDetails(AppDbContext db)
{
    var details = db.EmployeeDetails.ToList();

    Console.WriteLine("\n=== VIEW EMPLOYEE DETAILS ===");
    foreach (var emp in details)
    {
        Console.WriteLine($"Id: {emp.EmployeeId} - Name: {emp.EmployeeName} - Salary: {emp.Salary} - Dept: {emp.DepartmentName}");
    }
}

void ShowEmployeesByDept(AppDbContext db, int deptId)
{
    var result = db.EmployeeResults
        .FromSqlInterpolated($"EXEC GetEmployeesByDepartment @DepartmentId = {deptId}")
        .ToList();

    Console.WriteLine($"\n=== STORED PROC: EMPLOYEES IN DEPT {deptId} ===");
    foreach (var emp in result)
    {
        Console.WriteLine($"Id: {emp.Id} - Name: {emp.Name} - Salary: {emp.Salary}");
    }
}

void UpdateSalary(AppDbContext db, int empId, decimal newSalary)
{
    db.Database.ExecuteSqlInterpolated($"EXEC UpdateEmployeeSalary @EmployeeId = {empId}, @NewSalary = {newSalary}");

    var updatedEmp = db.Employees.Find(empId);

    Console.WriteLine($"\n=== UPDATED SALARY FOR EMP {empId} ===");
    Console.WriteLine($"Name: {updatedEmp.Name} - New Salary: {updatedEmp.Salary}");
}
