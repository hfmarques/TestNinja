using TestNinja.Mocking;

namespace TestNinja.UnitTests.Fundamentals;

public class FakeFileReader : IFileReader
{
    public string Read(string path)
    {
        return "";
    }
}