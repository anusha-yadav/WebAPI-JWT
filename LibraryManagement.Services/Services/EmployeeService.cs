using LibraryManagement.Repository.Contracts;
using LibraryManagement.Common;
using LibraryManagement.Services.Contracts;

namespace LibraryManagement.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository EmployeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository) 
        {
            EmployeeRepository = employeeRepository;
        }

        public List<EmployeeViewModel> GetEmployeeDetails()
        {
            return EmployeeRepository.GetEmployeeDetails();
        }
        public EmployeeViewModel GetEmployeeDetails(int id)
        {
            return EmployeeRepository.GetEmployeeDetails(id);
        }
        public void AddEmployee(EmployeeViewModel employee)
        {
            EmployeeRepository.AddEmployee(employee);
        }
        public void UpdateEmployee(EmployeeViewModel employee)
        {
            EmployeeRepository.UpdateEmployee(employee);
        }
        public void DeleteEmployee(int id)
        {
            EmployeeRepository.DeleteEmployee(id);
        }
        public bool CheckEmployee(int id)
        {
            return EmployeeRepository.CheckEmployee(id);
        }
    }
}
