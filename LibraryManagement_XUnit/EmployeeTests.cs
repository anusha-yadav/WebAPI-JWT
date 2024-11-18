using LibraryManagement.Common;
using LibraryManagement.Controllers;
using LibraryManagement.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LibraryManagement_XUnit
{
    public class EmployeeControllerTests
    {
        private readonly Mock<IEmployeeService> _mockEmployeeService;
        private readonly EmployeeController _controller;

        public EmployeeControllerTests()
        {
            _mockEmployeeService = new Mock<IEmployeeService>();
            _controller = new EmployeeController(_mockEmployeeService.Object);
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WithListOfEmployees()
        {
            var employeeList = new List<EmployeeViewModel> { new EmployeeViewModel(), new EmployeeViewModel() };
            _mockEmployeeService.Setup(service => service.GetEmployeeDetails())
                .Returns(employeeList);

            var result = await _controller.Get();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<EmployeeViewModel>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task Get_ById_ReturnsOkResult_WithEmployee()
        {
            // Arrange
            var employee = new EmployeeViewModel();
            _mockEmployeeService.Setup(service => service.GetEmployeeDetails(It.IsAny<int>()))
                .Returns(employee);

            // Act
            var result = await _controller.Get(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<EmployeeViewModel>(okResult.Value);
            Assert.NotNull(returnValue);
        }

        [Fact]
        public async Task Get_ById_ReturnsNotFound_WhenEmployeeNotExists()
        {
            // Arrange
            _mockEmployeeService.Setup(service => service.GetEmployeeDetails(It.IsAny<int>()))
                .Returns((EmployeeViewModel)null);

            // Act
            var result = await _controller.Get(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Get_ById_ReturnsOk_WithEmployeeResponse()
        {
            var employee = new EmployeeViewModel
            {
                EmployeeId = 1,
                EmployeeName = "John",
                Company = "XYZ"
            };

            _mockEmployeeService.Setup(service => service.GetEmployeeDetails(1)).Returns(employee);

            var result = await _controller.Get(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<EmployeeViewModel>(okResult.Value);
            Assert.Equal(employee.EmployeeId, returnValue.EmployeeId);
            Assert.Equal(employee.EmployeeName, returnValue.EmployeeName);
            Assert.Equal(employee.Company, returnValue.Company);
        }
    }
}