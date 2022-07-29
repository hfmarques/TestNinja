using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests;

[TestFixture]
public class DemeritPointsCalculatorTests
{
    private DemeritPointsCalculator calculator;

    [SetUp]
    public void SetUp()
    {
        calculator = new DemeritPointsCalculator();
    }
    [Test]
    [TestCase(-1)]
    [TestCase(301)]
    public void CalculateDemeritPoints_WhenSpeedIsBellow0OrGreaterThanMaxSpeed_ThrowArgumentOutOfRangeException(int speed)
    {
        Assert.That(() => calculator.CalculateDemeritPoints(speed), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
    }
    
    [Test]
    [TestCase(0,0)]
    [TestCase(10,0)]
    [TestCase(65,0)]
    [TestCase(66,0)]
    [TestCase(70,1)]
    [TestCase(75,2)]
    public void CalculateDemeritPoints_WhenCalled_ReturnDemeritPoints(int speed, int expectedResult)
    {
        var result = calculator.CalculateDemeritPoints(speed);
        
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}