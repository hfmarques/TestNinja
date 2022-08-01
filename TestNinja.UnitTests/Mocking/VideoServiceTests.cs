using NUnit.Framework;
using TestNinja.Mocking;
using TestNinja.UnitTests.Fundamentals;

namespace TestNinja.UnitTests.Mocking;

[TestFixture]
public class VideoServiceTests
{
    [Test]
    public void ReadVideoTitle_EmptyFile_ReturnError()
    {
        var service = new VideoService(new FakeFileReader());

        var result = service.ReadVideoTitle();
        
        Assert.That(result, Does.Contain("error").IgnoreCase);
    }
}