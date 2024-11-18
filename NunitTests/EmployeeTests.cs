using LibraryManagement.Common;
using LibraryManagement.Controllers;
using LibraryManagement.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace NunitTests
{
    public class EmployeeTests
    {
        private EmployeeController _controller;
        private Mock<IEmployeeService> _mockEmployeeService;

        [SetUp]
        public void Setup()
        {
            // Mock IEmployeeService
            _mockEmployeeService = new Mock<IEmployeeService>();

            // Initialize controller with mocked service
            _controller = new EmployeeController(_mockEmployeeService.Object);
        }

        [Test]
        public async Task Get_ReturnsOkResult_WithEmployees()
        {
            // Arrange
            var expectedEmployees = new List<EmployeeViewModel>
        {
            new EmployeeViewModel { EmployeeName = "John", Company = "XYZ", JobTitle="ASE" },
            new EmployeeViewModel { EmployeeName = "John", Company = "XYZ", JobTitle="ASE" }
        };
            _mockEmployeeService.Setup(service => service.GetEmployeeDetails()).Returns(expectedEmployees);

            // Act
            var result = await _controller.Get();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var actualEmployees = okResult.Value as List<EmployeeViewModel>;
            Assert.IsNotNull(actualEmployees);
            Assert.AreEqual(expectedEmployees.Count, actualEmployees.Count);
            for (int i = 0; i < expectedEmployees.Count; i++)
            {
                Assert.AreEqual(expectedEmployees[i].EmployeeId, actualEmployees[i].EmployeeId);
                Assert.AreEqual(expectedEmployees[i].EmployeeName, actualEmployees[i].EmployeeName);
                Assert.AreEqual(expectedEmployees[i].JobTitle, actualEmployees[i].JobTitle);
                Assert.AreEqual(expectedEmployees[i].Company, actualEmployees[i].Company);
            }
        }

        [Test]
        public async Task Get_ReturnsNotFound_WhenEmployeeNotFound()
        {
            // Arrange
            int nonExistingEmployeeId = 100;
            // Act
            var result = await _controller.Get(nonExistingEmployeeId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }
    }
}