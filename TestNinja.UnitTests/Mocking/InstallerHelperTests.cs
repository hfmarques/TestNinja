using System.Net;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking;

[TestFixture]
public class InstallerHelperTests
{
    private Mock<IFileDownloader> fileDownloader;
    private InstallerHelper installerHelper;

    [SetUp]
    public void SetUp()
    {
        fileDownloader = new Mock<IFileDownloader>();
        installerHelper = new InstallerHelper(fileDownloader.Object);
    }

    [Test]
    public void DownloadInstaller_DownloadFails_ReturnsFalse()
    {
        fileDownloader.Setup(x => x.DownloadFile(It.IsAny<string>(), It.IsAny<string>())).Throws<WebException>();

        var result = installerHelper.DownloadInstaller("", "");

        Assert.That(result, Is.False);
    }

    [Test]
    public void DownloadInstaller_DownloadSucceed_ReturnsTrue()
    {
        var result = installerHelper.DownloadInstaller("", "");

        Assert.That(result, Is.EqualTo(true));
    }
}