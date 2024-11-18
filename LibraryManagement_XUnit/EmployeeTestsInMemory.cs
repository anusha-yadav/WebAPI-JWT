using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Common;
using LibraryManagement.Controllers;
using LibraryManagement.Data.Entities;
using LibraryManagement.Data;
using LibraryManagement.Services.Services;
using LibraryManagement.Repository.Services;

namespace LibraryManagement_XUnit
{
    public class EmployeeTestsInMemory
    {
        private readonly EmployeeDBContext _context;
        private readonly EmployeeService _service;
        private readonly EmployeeController _controller;
        private readonly EmployeeRepository _repository;
        public EmployeeTestsInMemory()
        {
            var options = new DbContextOptionsBuilder<EmployeeDBContext>()
                .UseInMemoryDatabase(databaseName: "LibraryManagement")
                .Options;

            _context = new EmployeeDBContext(options);
            _service = new EmployeeService(new EmployeeRepository(_context));
            _controller = new EmployeeController(_service);

            SeedTestData();
        }

        private void SeedTestData()
        {
            _context.Employees.Add(new Employee { EmployeeName = "John", Company = "XYZ", JobTitle="ASE" });
            _context.Employees.Add(new Employee { EmployeeName = "Jane", Company = "XYZ", JobTitle = "ASE" });
            _context.SaveChanges();
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WithListOfEmployees()
        {
            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<EmployeeViewModel>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task Get_ById_ReturnsOkResult_WithEmployee()
        {
            // Act
            var result = await _controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<EmployeeViewModel>(okResult.Value);
            Assert.Equal(1, returnValue.EmployeeId);
            Assert.Equal("John", returnValue.EmployeeName);
            Assert.Equal("XYZ", returnValue.Company);
            Assert.Equal("ASE",returnValue.JobTitle);
        }

        [Fact]
        public async Task Get_ById_ReturnsNotFound_WhenEmployeeNotExists()
        {
            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsOk_WithEmployeeResponse()
        {
            // Arrange
            var newEmployee = new EmployeeViewModel
            {
                EmployeeId = 3,
                EmployeeName = "Sam",
                Company = "XYZ",
                JobTitle = "ASE"
            };

            // Act
            var result = await _controller.AddEmployee(newEmployee);

            // Assert
            var okResult = Assert.IsType<ActionResult<EmployeeViewModel>>(result);
            var returnValue = Assert.IsType<EmployeeViewModel>(okResult.Value);
            Assert.Equal(newEmployee.EmployeeId, returnValue.EmployeeId);
            Assert.Equal(newEmployee.EmployeeName, returnValue.EmployeeName);
        }
    }

}
