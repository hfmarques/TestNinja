using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking;

[TestFixture]
public class EmployeeControllerTests
{
    private Mock<IEmployeeRepository> employeeRepository;
    private EmployeeController employeeController;

    [SetUp]
    public void SetUp()
    {
        employeeRepository = new Mock<IEmployeeRepository>();
        employeeController = new EmployeeController(employeeRepository.Object);
    }

    [Test]
    public void DeleteEmployee_WhenCalled_EmployeeIsDeleted()
    {
        employeeController.DeleteEmployee(1);
        
        employeeRepository.Verify(s => s.Delete(1)); 
    }
    
    [Test]
    public void DeleteEmployee_WhenCalled_ReturnsRedirectResult()
    {
        var result = employeeController.DeleteEmployee(1);
        
        Assert.That(result, Is.TypeOf<RedirectResult>()); 
    }
}