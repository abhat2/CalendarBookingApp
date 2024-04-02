using CalendarBookingApp.Data;
using CalendarBookingApp.Helper;
using CalendarBookingApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            // Configuration
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();

            // Database
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            var context = new AppDbContext(optionsBuilder.Options);
            context.Database.EnsureCreated();

            // Services
            var services = new ServiceCollection()
                .AddSingleton(configuration)
                .AddSingleton(optionsBuilder.Options)
                .AddSingleton<IAppointmentService, AppointmentService>()
                .AddDbContextPool<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
                .BuildServiceProvider();

            var service = services.GetService<IAppointmentService>();

            Console.WriteLine("Welcome to the Calendar Booking app!");
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Instructions");
            Console.WriteLine("1. To add an appointment, enter ADD DD/MM hh:mm");
            Console.WriteLine("2. To delete an appointment, enter DELETE DD/MM hh:mm");
            Console.WriteLine("3. To find an appointment, enter FIND DD/MM hh:mm");
            Console.WriteLine("4. To keep a timeslot for any day, KEEP hh:mm");

            Console.WriteLine("What would you like to do?");
            string? input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Input is null or empty. Please refer to instructions.");
            }
            else
            {
                if (input.Contains(Constants.AddAppointment) || input.Contains(Constants.DeleteAppointment) || input.Contains(Constants.FindAppointment) || input.Contains(Constants.KeepAppointment))
                {
                    // Add appointment
                    if (input.Contains(Constants.AddAppointment))
                    {
                        string timeString = input.Split($"{Constants.AddAppointment} ")[1];

                        DateTime startTime = Validator.GetDateTime(timeString);
                        bool validStartTime = Validator.CheckValidDateTime(startTime);

                        if (validStartTime)
                        {
                            DateTime endTime = startTime.AddMinutes(Constants.AppointmentDuration);

                            Appointment appointment = new Appointment { AppointmentStartTime= startTime, AppointmentEndTime = endTime };
                            service.AddAppointment(appointment);
                        }
                    }

                    // Delete appointment
                    if (input.Contains(Constants.DeleteAppointment))
                    {
                        string timeString = input.Split($"{Constants.DeleteAppointment} ")[1];

                        DateTime startTime = Validator.GetDateTime(timeString);
                        service.DeleteAppointment(startTime);
                    }

                    // Find appointment
                    if (input.Contains(Constants.FindAppointment))
                    {
                        string timeString = input.Split($"{Constants.FindAppointment} ")[1];

                        DateTime startTime = Validator.GetDateTime(timeString);
                        service.FindAppointment(startTime);
                    }

                    // Keep appointment
                    if (input.Contains(Constants.KeepAppointment))
                    {
                        string timeString = input.Split($"{Constants.KeepAppointment} ")[1];

                        DateTime startTime = Validator.GetRandomDateTime(timeString);
                        bool validStartTime = Validator.CheckValidDateTime(startTime);

                        if (validStartTime)
                        {
                            DateTime endTime = startTime.AddMinutes(Constants.AppointmentDuration);

                            Appointment appointment = new Appointment { AppointmentStartTime = startTime, AppointmentEndTime = endTime };
                            service.AddAppointment(appointment);
                        }

                    }
                }
                else
                {
                    Console.WriteLine("Invalid input provided. Input must contain the keyword ADD, DELETE, FIND or KEEP.");
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }   
}