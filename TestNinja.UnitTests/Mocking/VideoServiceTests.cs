using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking;

[TestFixture]
public class VideoServiceTests
{
    private Mock<IFileReader> fileReader;
    private Mock<IVideoRepository> videoRepository;
    private VideoService service;

    [SetUp]
    public void SetUp()
    {
        fileReader = new Mock<IFileReader>();
        videoRepository = new Mock<IVideoRepository>();
        service = new VideoService(fileReader.Object, videoRepository.Object);
    }

    [Test]
    public void ReadVideoTitle_EmptyFile_ReturnError()
    {
        fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

        var result = service.ReadVideoTitle();

        Assert.That(result, Does.Contain("error").IgnoreCase);
    }

    [Test]
    public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnsEmptyString()
    {
        videoRepository.Setup(x => x.GetUnprocessedVideos()).Returns(new List<Video>());

        var result = service.GetUnprocessedVideosAsCsv();

        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void GetUnprocessedVideosAsCsv_SomeVideosUnprocessed_ReturnsUnprocessedIds()
    {
        videoRepository.Setup(x => x.GetUnprocessedVideos()).Returns(new List<Video>
        {
            new() {Id = 1, Title = "test1", IsProcessed = false},
            new() {Id = 2, Title = "test2", IsProcessed = false},
            new() {Id = 3, Title = "test3", IsProcessed = false}
        });

        var result = service.GetUnprocessedVideosAsCsv();

        Assert.That(result, Is.EqualTo("1,2,3"));
    }
}