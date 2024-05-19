using LibraryManagement.Data;
using LibraryManagement.Data.Entities;
using LibraryManagement.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Repository.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public readonly EmployeeDBContext DbContext;

        public EmployeeRepository(EmployeeDBContext dbContext)
        {
            DbContext = dbContext;
        }

        public List<Employee> GetEmployeeDetails()
        {
            try
            {
                return DbContext.Employees.ToList();
            }
            catch
            {
                throw;
            }
        }

        public Employee GetEmployeeDetails(int id)
        {
            try
            {
                Employee? employee = DbContext.Employees.Find(id);
                if (employee != null)
                {
                    return employee;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public void AddEmployee(Employee employee)
        {
            try
            {
                DbContext.Employees.Add(employee);
                DbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            try
            {
                DbContext.Entry(employee).State = EntityState.Modified;
                DbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Employee DeleteEmployee(int id)
        {
            try
            {
                Employee? employee = DbContext.Employees.Find(id);

                if (employee != null)
                {
                    DbContext.Employees.Remove(employee);
                    DbContext.SaveChanges();
                    return employee;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public bool CheckEmployee(int id)
        {
            return DbContext.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
