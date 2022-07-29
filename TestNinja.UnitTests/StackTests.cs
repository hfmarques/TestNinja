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
    public void Count_EmptyStack_Return0()
    {
        Assert.That(stack.Count, Is.EqualTo(0));
    }
    
    [Test]
    public void Push_ObjectIsNull_ThrowArgumentNullException()
    {
        Assert.That(() => stack.Push(null!), Throws.ArgumentNullException);
    }
    
    [Test]
    public void Push_ValidObject_AddObjectToStack()
    {
        stack.Push(new Customer());
        Assert.That(stack.Count, Is.EqualTo(1));
    }
    
    [Test]
    public void Pop_StackEmpty_ThrowInvalidOperationException()
    {
        Assert.That(() => stack.Pop(), Throws.InvalidOperationException);
    }

    [Test]
    public void Pop_StackWithFewObjects_ReturnsObjectOnTheTop()
    {
        var customer1 = new Customer();
        var customer2 = new Customer();
        var customer3 = new Customer();
        var customer4 = new Customer();

        stack.Push(customer1);
        stack.Push(customer2);
        stack.Push(customer3);
        stack.Push(customer4);

        var result = stack.Pop();
        
        Assert.That(result, Is.EqualTo(customer4));
    }
    
    [Test]
    public void Pop_StackWithFewObjects_RemoveObjectOnTheTop()
    {
        var customer1 = new Customer();
        var customer2 = new Customer();
        var customer3 = new Customer();
        var customer4 = new Customer();

        stack.Push(customer1);
        stack.Push(customer2);
        stack.Push(customer3);
        stack.Push(customer4);

        stack.Pop();
        var result = stack.Peek();
        
        Assert.That(result, Is.Not.EqualTo(customer4));
        Assert.That(stack.Count, Is.EqualTo(3));
    }
    
    [Test]
    public void Peek_EmptyStack_ThrowInvalidOperationException()
    {
        Assert.That(() => stack.Peek(), Throws.InvalidOperationException);
    }
    
    [Test]
    public void Peek_StackWithObjects_ReturnsObjectOnTopOfStack()
    {
        var customer1 = new Customer();
        var customer2 = new Customer();
        var customer3 = new Customer();
        var customer4 = new Customer();

        stack.Push(customer1);
        stack.Push(customer2);
        stack.Push(customer3);
        stack.Push(customer4);

        var result = stack.Peek();
        
        Assert.That(result, Is.EqualTo(customer4));
    }
    
    [Test]
    public void Peek_StackWithObjects_DoesNotRemoveTheObjectOnTopOfStack()
    {
        var customer1 = new Customer();
        var customer2 = new Customer();
        var customer3 = new Customer();
        var customer4 = new Customer();

        stack.Push(customer1);
        stack.Push(customer2);
        stack.Push(customer3);
        stack.Push(customer4);

        stack.Peek();
        
        Assert.That(stack.Count, Is.EqualTo(4));
    }
}