namespace HotelReservation.Models;

public sealed class BookingDetails
{
    public string Hotel { get; }
    public int RoomNumber { get; }

    public BookingDetails(string hotel, int roomNumber)
    {
        Hotel = hotel;
        RoomNumber = roomNumber;
    }
    public override bool Equals(object? obj)
    {
        if (obj is not BookingDetails details) return false;
        return Hotel == details.Hotel && RoomNumber == details.RoomNumber;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Hotel, RoomNumber);
    }
}
