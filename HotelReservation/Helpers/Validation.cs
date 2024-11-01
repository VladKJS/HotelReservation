namespace HotelReservation.Helpers;

public static class Validation
{
    public static bool Validator(this string booking, out BookingDetails reservation)
    {
        if (!ValidateStringFormat(booking, out int hyphenIndex))
        {
            throw new BadRequestException(400,
                "Booking must be a non-empty string, in a valid format ex.'Hilton-Room1'.");
        }

        var hotel = booking.Substring(0, hyphenIndex);
        hotel = CapitalizeFirstLetter(hotel);

        if (!IsHotelValid(hotel))
        {
            throw new BadRequestException(400, "Booking must be for a valid hotel in the system.");
        }

        var room = booking.Substring(hyphenIndex + 1);

        if (!TryGetRoomNumber(room, out var roomNumber))
        {
            throw new BadRequestException(400, "Room number must be in a valid format ex.'Room1'.");
        }

        reservation = new(hotel, roomNumber);

        return true;
    }
    private static bool ValidateStringFormat(string booking, out int hyphenIndex)
    {
        hyphenIndex = 0;
        if (string.IsNullOrWhiteSpace(booking) || !booking.Contains('-'))
        {
            return false;           
        }

            hyphenIndex = booking.IndexOf('-');

        if (hyphenIndex <= 0 || hyphenIndex >= booking.Length - 1 || string.IsNullOrWhiteSpace(booking.Substring(0, hyphenIndex)) ||
            string.IsNullOrWhiteSpace(booking.Substring(hyphenIndex + 1)))
        {
            return false;            
        }
        return true;
    }
    private static bool IsHotelValid(string hotelName)
    {
        return Enum.TryParse(typeof(Hotels), hotelName, true, out _); //ignoreCase true/false
    }
    private static string CapitalizeFirstLetter(string str)
    {
        var hot = str.Trim();
        return char.ToUpper(hot[0]) + hot.Substring(1);
    }
    private static bool TryGetRoomNumber(string room, out int roomNumber)
    {
        if (Regex.IsMatch(room, @"^\s*room\s*\d+(\s*\d+)*\s*$", RegexOptions.IgnoreCase))
        {      
            string numbers = string.Concat(Regex.Matches(room, @"\d+").Select(m => m.Value));
            return int.TryParse(numbers, out roomNumber);                   
        }
        roomNumber = 0;
        return false;
    }
}
