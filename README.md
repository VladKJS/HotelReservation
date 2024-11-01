# HotelReservation API

An API for a reservation system at a chain of hotels: Clarion, Hilton, and GrandHotel.  
The application has only one endpoint: "/reservation".

## Technical Information

- ASP.NET Core 8 application.
- The application is using the Minimal API .NET template.

### Namespaces And Their Purposes

- **Data**  
  Storing reservation data.
  
- **Extensions**
  Using a local (not distributed on NuGet.org, attached to the project as zip file) NuGet package for:  
  - **Configuring Swagger extension.**
  
- **Helpers**  
  Validation Helper to validate the incoming request, which should be a string with the format 'HotelName-RoomNumber' (e.g., 'Hilton-Room101').  
  If the validation is successful, a new 'BookingDetails' object is created. If validation fails, a 'BadRequestException' is thrown.
  
- **Models**  
  Containing objects and enums used in the application.
  
- **Services**  
  Booking service.
  
- **Middleware**  
  Using a local (not distributed on NuGet.org, attached to the project as zip file) NuGet package for:
  - **Global Exception Handling**  
    Using `IExceptionHandlerFeature` for exception handling. When an error occurs in the application, it is caught and redirected to an internal endpoint "/error". The middleware then uses the `Microsoft.AspNetCore.Http` - 'Results' class and 'ProblemDetails' service to customize the problem details response.
    
  - **Serilog**  
    Serilog middleware with a custom enricher.

#### Responses

- **Successful reservation:**  
  - Status Code: 200  
  - Message: true
  
- **Unsuccessful reservation due to an unavailable room:**  
  - Status Code: 200  
  - Message: false
  
- **Error Response**  
  Using a customized 'Results.Problem' object with properties:
  - `"type"`: link to RFC webpage
  - `"title"`: description of the issue
  - `"status"`: status code
  - `"detail"`: information about the cause of the error
  - `"Vlad"`: "NuGet"

- **Error responses can be:**  
  - 400 Bad Request  
  - 500 Internal Server Error

### Error Response Examples

- **Example of response when trying to make a reservation for a hotel not part of the hotel chain:**  
  When `Result.Problem` object is returned  `content-type` set as:  
  `'content-type: application/problem+json'`

  ```json
  {
    "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
    "title": "Bad Request",
    "status": 400,
    "detail": "Booking must be for a valid hotel in the system.",
    "Vlad": "NuGet"
  }
  ```

- **Internal server error response example**  
  ```json
  {
    "type": "https://tools.ietf.org/html/rfc9110#section-15.6.1",
    "title": "An error occurred while processing your request.",
    "status": 500,
    "Vlad": "NuGet"
  }
  ```

### Swagger Documentation

- Swagger is documented using:
  - **Summary**: "Book an available room in Clarion, Hilton, and GrandHotel. Format must be as 'HotelName-RoomNumber'."
  - **Description**: "Returns 'true' if a reservation is made successfully. Returns 'false' if the reservation could not be made."
