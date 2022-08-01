using Moq;
using NUnit.Framework;
using TestNinja.Mocking;
using TestNinja.UnitTests.Fundamentals;

namespace TestNinja.UnitTests.Mocking;

[TestFixture]
public class VideoServiceTests
{
    private Mock<IFileReader> fileReader;
    private VideoService service;

    [SetUp]
    public void SetUp()
    {
        fileReader = new Mock<IFileReader>();
        service = new VideoService(fileReader.Object);
    }

    [Test]
    public void ReadVideoTitle_EmptyFile_ReturnError()
    {
        fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

        var result = service.ReadVideoTitle();

        Assert.That(result, Does.Contain("error").IgnoreCase);
    }
}