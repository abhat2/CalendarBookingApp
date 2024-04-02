using CalendarBookingApp;
using CalendarBookingApp.Database;
using CalendarBookingApp.Helper;
using CalendarBookingApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
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
                Validator.ValidateUserInput(input);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }   
}