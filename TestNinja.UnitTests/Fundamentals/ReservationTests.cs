using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals;

[TestFixture]
public class ReservationTests
{
    [Test]
    public void CanBeCancelledBy_AdminCancelling_ReturnsTrue()
    {
        //Arrange
        var reservation = new Reservation();

        //Act
        var result = reservation.CanBeCancelledBy(new User {IsAdmin = true});

        //Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CanBeCancelledBy_SameUserCancelling_ReturnsTrue()
    {
        var user = new User();
        var reservation = new Reservation() {MadeBy = user};

        var result = reservation.CanBeCancelledBy(user);

        Assert.That(result == true);
    }

    [Test]
    public void CanBeCancelledBy_AnotherUserCancelling_ReturnsFalse()
    {
        var user = new User();
        var reservation = new Reservation {MadeBy = new User()};

        var result = reservation.CanBeCancelledBy(user);

        Assert.IsFalse(result);
    }
}