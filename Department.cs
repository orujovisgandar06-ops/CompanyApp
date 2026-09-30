using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyApp
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}