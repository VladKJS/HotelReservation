
ConfigureAndCreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((_, x) =>
{
    x.ReadFrom.Configuration(builder.Configuration);
    x.Enrich.With<CustomEnricher>();
    x.WriteTo.Console();
});
builder.Services.AddEndpointsApiExplorer();
var assemblyName = Assembly.GetExecutingAssembly().GetName().Name!;
builder.Services.AddSwaggerDocumentation(assemblyName);
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = null;
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddSwaggerGen(options =>options.EnableAnnotations());
builder.Services.AddGlobalErrorHandling();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IReservationData, ReservationData>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseMiddleware<SerilogMiddleware>();
app.UseGlobalErrorHandling();
app.UseSwaggerDocumentation(assemblyName);

app.MapPost("/reservation",([FromBody]Booking booking, IBookingService bookingService) =>
{
    var newBooking = booking.Reservation.Validator();

    var result = bookingService.MakeReservation(new BookingDetails(newBooking.Hotel, newBooking.RoomNumber));
 
    return result;
})
.WithName("BookAvailableRoom")
.Accepts<Booking>("application/json")
.Produces<bool>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status400BadRequest, typeof(ProblemDetails), "application/problem+json")
.Produces(StatusCodes.Status500InternalServerError, typeof(ProblemDetails), "application/problem+json")
.WithMetadata(new SwaggerOperationAttribute(
    "Book an available room in Carlton, Hilton and GrandHotel.",
    "Returns 'true' if a reservation is made successfully. Returns 'false' if the reservation could not be made."
 ));

try
{
    Log.Information("Starting host: {Application} {Environment}", builder.Environment.ApplicationName, builder.Environment.EnvironmentName);
    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Host terminated unexpectedly. {ex.Message}");
    throw;
}
finally
{
    Log.CloseAndFlush();
}
static void ConfigureAndCreateLogger()
{
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .CreateLogger();
}
public partial class Program { }