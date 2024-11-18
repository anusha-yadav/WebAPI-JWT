using LibraryManagement.Common;

namespace LibraryManagement.Services.Contracts
{
    public interface IEmployeeService
    {
        public List<EmployeeViewModel> GetEmployeeDetails();
        public EmployeeViewModel GetEmployeeDetails(int id);
        public void AddEmployee(EmployeeViewModel employee);
        public void UpdateEmployee(EmployeeViewModel employee);
        public void DeleteEmployee(int id);
        public bool CheckEmployee(int id);
    }
}
