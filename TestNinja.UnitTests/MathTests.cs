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

    [Test]
    public void GetOddNumbers_LimitIsGreaterThanZero_ReturnOddNumbersUpToLimit()
    {
        var result = math.GetOddNumbers(5);
        
        // Assert.That(result, Is.Not.Empty);
        // Assert.That(result.Count(), Is.EqualTo(3));
        
        // Assert.That(result, Does.Contain(1));
        // Assert.That(result, Does.Contain(3));
        // Assert.That(result, Does.Contain(5));
        
        Assert.That(result, Is.EquivalentTo(new [] {1,3,5}));
        
        // Assert.That(result, Is.Ordered);
        // Assert.That(result, Is.Unique);
    }
    
    [Test]
    public void GetOddNumbers_LimitIsZero_ReturnEmptyArray()
    {
        var result = math.GetOddNumbers(0);
        
        Assert.That(result, Is.Empty);
    }
}