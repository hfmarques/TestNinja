using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking;

[TestFixture]
public class HousekeeperHelperTests
{
    private Mock<IHousekeeperRepository> housekeeperRepository;
    private Mock<IEmailSender> emailSender;
    private Mock<IStatementGenerator> statementGenerator;
    private Mock<IXtraMessageBox> xtraMessageBox;
    private HousekeeperService housekeeperService;
    private Housekeeper housekeeper;
    private DateTime date;
    private string fileName;

    [SetUp]
    public void SetUp()
    {
        housekeeperRepository = new Mock<IHousekeeperRepository>();
        emailSender = new Mock<IEmailSender>();
        statementGenerator = new Mock<IStatementGenerator>();
        xtraMessageBox = new Mock<IXtraMessageBox>();

        housekeeperService = new HousekeeperService(housekeeperRepository.Object, statementGenerator.Object,
            emailSender.Object, xtraMessageBox.Object);

        housekeeper = new Housekeeper
        {
            FullName = "John Doe",
            Email = "JohnDoe@email.com",
            Oid = 123456,
            StatementEmailBody = "Hi, this is a test"
        };

        date = DateTime.Now;
        fileName = "fileName";

        housekeeperRepository.Setup(x => x.GetAll()).Returns(new List<Housekeeper> {housekeeper});
        statementGenerator.Setup(x => x.SaveStatement(housekeeper.Oid, housekeeper.FullName, date))
            .Returns(() => fileName); //lazy evaluation
    }

    [Test]
    [TestCase(null)]
    [TestCase("  ")]
    [TestCase("")]
    public void SendStatementEmails_HousekeeperEmailIsNotRight_AssertThatSaveStatementIsNotCalled(string email)
    {
        housekeeper.Email = email;
        var result = housekeeperService.SendStatementEmails(date);
        statementGenerator.Verify(x => x.SaveStatement(housekeeper.Oid, housekeeper.FullName, date), Times.Never);
        Assert.That(result, Is.True);
    }

    [Test]
    public void SendStatementEmails_WhenCalled_AssertThatSaveStatementIsCalled()
    {
        var result = housekeeperService.SendStatementEmails(date);
        statementGenerator.Verify(x => x.SaveStatement(housekeeper.Oid, housekeeper.FullName, date));
        Assert.That(result, Is.True);
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase("  ")]
    public void SendStatementEmails_WhenCalled_AssertThatEmailFileIsNotCalled(string fileNameReturned)
    {
        fileName = fileNameReturned;
        var result = housekeeperService.SendStatementEmails(date);
        emailSender.Verify(x =>
                x.EmailFile(housekeeper.Email,
                    housekeeper.StatementEmailBody,
                    fileName,
                    It.IsAny<string>()),
            Times.Never);
        Assert.That(result, Is.True);
    }

    [Test]
    public void SendStatementEmails_WhenCalled_AssertThatEmailFileIsCalled()
    {
        var result = housekeeperService.SendStatementEmails(date);
        emailSender.Verify(x =>
            x.EmailFile(housekeeper.Email,
                housekeeper.StatementEmailBody,
                fileName,
                It.IsAny<string>()));
        Assert.That(result, Is.True);
    }

    [Test]
    public void SendStatementEmails_WhenCalled_ReturnFalse()
    {
        emailSender.Setup(x =>
                x.EmailFile(housekeeper.Email,
                    housekeeper.StatementEmailBody,
                    fileName,
                    It.IsAny<string>()))
            .Throws<Exception>();

        var result = housekeeperService.SendStatementEmails(date);

        xtraMessageBox.Verify(x =>
            x.Show(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<MessageBoxButtons>()));

        Assert.That(result, Is.False);
    }
}