using System.Text.RegularExpressions;

namespace HotelReservation.Helpers;

public static class Validation
{
    public static BookingDetails Validator(this string booking)
    {

        if (string.IsNullOrWhiteSpace(booking) || !booking.Contains('-'))
        {
            throw new BadRequestException(400, "Booking must be a non-empty string containing a hyphen.");
        }

        int hyphenIndex = booking.IndexOf('-');

        if (hyphenIndex == 0 || hyphenIndex == booking.Length - 1)
        {
            throw new BadRequestException(400, "Booking must contain a valid hotel name and room number separated by a hyphen.");
        }

        var hotel = booking.Substring(0, hyphenIndex);
        hotel = FirstLetterToUpper(hotel);

        if (!IsHotelValid(hotel))
        {
            throw new BadRequestException(400, "Booking must be for a valid hotel in the system.");
        }

        var room = booking.Substring(hyphenIndex + 1);

        var roomNumber = GetRoomNumber(room);

        BookingDetails reservation = new(hotel, roomNumber);

        return reservation;
    }
    private static bool IsHotelValid(string hotelName)
    {
        return Enum.TryParse(typeof(Hotels), hotelName, true, out _); //ignoreCase true/false
    }
    private static string FirstLetterToUpper(string str)
    {
        var hot = str.Trim();
        return char.ToUpper(hot[0]) + hot.Substring(1);
    }
    private static int GetRoomNumber(string room)
    {
        if (Regex.IsMatch(room, @"^\s*room\s*\d+(\s*\d+)*\s*$", RegexOptions.IgnoreCase))
        {      
            string numbers = string.Concat(Regex.Matches(room, @"\d+").Select(m => m.Value));
            if (!int.TryParse(numbers, out var roomNumber))
            {
                throw new BadRequestException(400, "Room number must be a valid integer.");
            }
            return roomNumber;
        }
        throw new BadRequestException(400, "Room number must be in a valid format ex.'Room1'.");
    }
}
