using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests;

[TestFixture]
public class StackTests
{
    private TestNinja.Fundamentals.Stack<Customer> stack;
    [SetUp]
    public void SetUp()
    {
        stack = new TestNinja.Fundamentals.Stack<Customer>();
    }

    [Test]
    public void Push_ObjectIsNull_ThrowArgumentNullException()
    {
        Assert.That(() => stack.Push(null!), Throws.ArgumentNullException);
    }
    
    [Test]
    public void Push_Add1Object_CountEqualsTo1()
    {
        stack.Push(new Customer());
        Assert.That(stack.Count, Is.EqualTo(1));
    }
    
    [Test]
    public void Pop_StackCountEqualsToZero_ThrowInvalidOperationException()
    {
        Assert.That(() => stack.Pop(), Throws.InvalidOperationException);
    }

    [Test]
    public void Pop_RemoveInsertedObject_ReturnsInsertedObject()
    {
        var customer = new Customer();

        stack.Push(customer);

        var result = stack.Pop();
        
        Assert.That(result, Is.EqualTo(customer));
    }
    
    [Test]
    public void Peek_StackCountEqualsToZero_ThrowInvalidOperationException()
    {
        Assert.That(() => stack.Peek(), Throws.InvalidOperationException);
    }
    
    [Test]
    public void Peek_GetLastObjectOfTheStackWith1Object_ReturnsInsertedObject()
    {
        var customer = new Customer();

        stack.Push(customer);

        var result = stack.Peek();
        
        Assert.That(result, Is.EqualTo(customer));
    }
    
    [Test]
    public void Peek_GetLastObjectOfTheStackWith2Objects_ReturnsTheFirstInsertedObject()
    {
        var customer1 = new Customer();
        var customer2 = new Customer();

        stack.Push(customer1);
        stack.Push(customer2);

        var result = stack.Peek();
        
        Assert.That(result, Is.EqualTo(customer2));
    }
}