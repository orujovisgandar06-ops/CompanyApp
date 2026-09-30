using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyApp
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public bool IsActive { get; set; }
    }
}