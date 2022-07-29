using NUnit.Framework;
using Math = TestNinja.Fundamentals.Math;

namespace TestNinja.UnitTests;

[TestFixture]
public class MathTests
{
    private Math math;
    
    [SetUp]
    public void SetUp()
    {
        math = new Math();
    }
    [Test]
    public void Add_WhenCalled_ReturnTheSumOfArguments()
    {
        var result = math.Add(1, 2);

        Assert.That(result, Is.EqualTo(3));
    }

    [Test]
    [TestCase(2,1,2)]
    [TestCase(1,2,2)]
    [TestCase(2,2,2)]
    public void Max_WhenCalled_ReturnGreaterArgument(int a, int b, int expectedResult)
    {
        var result = math.Max(a, b);
        
        Assert.That(result, Is.EqualTo(expectedResult));
    }
}