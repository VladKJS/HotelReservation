namespace HotelReservation.Services;

public interface IBookingService
{
    Task<bool> MakeReservation(BookingDetails bookingDetails);
}

public class BookingService(IReservationData reservationData) : IBookingService
{ 
    public Task<bool> MakeReservation(BookingDetails bookingDetails)
    {
        return reservationData.MakeReservation(bookingDetails);
    }
}