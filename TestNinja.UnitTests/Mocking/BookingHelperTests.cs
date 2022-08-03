using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking;

[TestFixture]
public class BookingHelperTests
{
    private Mock<IBookingRepository> bookingRepository;
    private Booking existingBooking;

    [SetUp]
    public void SetUp()
    {
        existingBooking = new Booking
        {
            Id = 2,
            ArrivalDate = ArriveOn(2022, 08, 15),
            DepartureDate = DepartureOn(2022, 08, 20),
            Reference = "a"
        };

        bookingRepository = new Mock<IBookingRepository>();
        bookingRepository.Setup(x => x.GetActiveBooks(1)).Returns(new List<Booking>
        {
            existingBooking
        }.AsQueryable());
    }

    [Test]
    public void OverlappingBookingsExist_BookingCancelled_ReturnEmptyString()
    {
        var result =
            BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(existingBooking.ArrivalDate),
                DepartureDate = Before(existingBooking.DepartureDate),
                Status = "Cancelled"
            }, bookingRepository.Object);

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void OverlappingBookingsExist_NoBookingsFound_ReturnEmptyString()
    {
        bookingRepository.Setup(x => x.GetActiveBooks(1)).Returns(new List<Booking>().AsQueryable());

        var result = BookingHelper.OverlappingBookingsExist(new Booking(), bookingRepository.Object);

        Assert.That(result, Is.EqualTo(""));
    }

    [Test]
    public void OverlappingBookingsExist_BookingStartAndFinishBeforeExistingBooking_ReturnEmptyString()
    {
        var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 2,
                ArrivalDate = Before(existingBooking.ArrivalDate,2),
                DepartureDate = Before(existingBooking.ArrivalDate),
                Reference = "book2"
            },
            bookingRepository.Object);


        Assert.That(result, Is.Empty);
    } 
    
    [Test]
    public void OverlappingBookingsExist_BookingStartAndFinishAfterExistingBooking_ReturnEmptyString()
    {
        var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 2,
                ArrivalDate = After(existingBooking.DepartureDate),
                DepartureDate = After(existingBooking.DepartureDate, 2),
                Reference = "book2"
            },
            bookingRepository.Object);


        Assert.That(result, Is.Empty);
    }

    [Test]
    public void OverlappingBookingsExist_BookingStartBeforeExistingBookAndFinishOnTheMiddle_ReturnOverlappingReference()
    {
        var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(existingBooking.ArrivalDate),
                DepartureDate = Before(existingBooking.DepartureDate)
            },
            bookingRepository.Object);


        Assert.That(result, Is.EqualTo(existingBooking.Reference));
    }

    [Test]
    public void OverlappingBookingsExist_BookingStartAfterExistingBookAndFinishOnTheMiddle_ReturnOverlappingReference()
    {
        var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(existingBooking.ArrivalDate),
                DepartureDate = Before(existingBooking.DepartureDate)
            },
            bookingRepository.Object);


        Assert.That(result, Is.EqualTo(existingBooking.Reference));
    }

    [Test]
    public void OverlappingBookingsExist_BookingStartAfterExistingBookAndFinishAfter_ReturnOverlappingReference()
    {
        var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(existingBooking.ArrivalDate),
                DepartureDate = After(existingBooking.DepartureDate)
            },
            bookingRepository.Object);


        Assert.That(result, Is.EqualTo(existingBooking.Reference));
    }

    private DateTime ArriveOn(int year, int month, int day)
    {
        return new DateTime(year, month, day, 14, 0, 0);
    }

    private DateTime DepartureOn(int year, int month, int day)
    {
        return new DateTime(year, month, day, 10, 0, 0);
    }

    private DateTime Before(DateTime dateTime, int days = 1)
    {
        return dateTime.AddDays(-days);
    }

    private DateTime After(DateTime dateTime, int days = 1)
    {
        return dateTime.AddDays(days);
    }
}