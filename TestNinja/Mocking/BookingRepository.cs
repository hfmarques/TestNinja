using System.Linq;

namespace TestNinja.Mocking
{
    public interface IBookingRepository
    {
        IQueryable<Booking> GetActiveBooks(int? excludedBookingId = null);
    }

    public class BookingRepository : IBookingRepository
    {
        public IQueryable<Booking> GetActiveBooks(int? excludedBookingId = null)
        {
            var unitOfWork = new UnitOfWork();
            var bookings = unitOfWork.Query<Booking>()
                .Where(
                    b => b.Status != "Cancelled");

            if (excludedBookingId.HasValue)
            {
                bookings = bookings.Where(b => b.Id != excludedBookingId);
            }

            return bookings;
        }
    }
}