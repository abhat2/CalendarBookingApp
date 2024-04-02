using CalendarBookingApp.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarBookingApp.Helper
{
    public class Validator
    {
        public static void ValidateUserInput(string input)
        {
            if (input.Contains(Constants.AddAppointment) || input.Contains(Constants.DeleteAppointment) || input.Contains(Constants.FindAppointment) || input.Contains(Constants.KeepAppointment))
            {
                // Add appointment
                if (input.Contains(Constants.AddAppointment))
                {
                    string timeString = input.Split($"{Constants.AddAppointment} ")[1];

                    DateTime startTime = GetDateTime(timeString);
                    bool validStartTime = CheckValidDateTime(startTime);

                    if (validStartTime)
                    {
                        DateTime endTime = startTime.AddMinutes(Constants.AppointmentDuration);

                        using (var context = new AppDbContext())
                        {
                            // creates db if not exists 
                            context.Database.EnsureCreated();

                            var appointment = new Appointment() { AppointmentStartTime = startTime, AppointmentEndTime = endTime };
                            context.Appointments.Add(appointment);

                            context.SaveChanges();

                            Console.WriteLine($"Appointment starting at {startTime} has been booked.");
                        }
                    }
                }

                // Delete appointment
                if (input.Contains(Constants.DeleteAppointment))
                {
                    string timeString = input.Split($"{Constants.DeleteAppointment} ")[1];

                    DateTime startTime = GetDateTime(timeString);

                    using (var context = new AppDbContext())
                    {
                        // creates db if not exists 
                        context.Database.EnsureCreated();

                        var appointment = context.Appointments.Where(a => a.AppointmentStartTime == startTime).FirstOrDefault();
                        if (appointment == null)
                        {
                            Console.WriteLine("No appointment was found.");
                        }
                        else
                        {
                            context.Remove(appointment);
                            context.SaveChanges();

                            Console.WriteLine($"Appointment starting at {startTime} has been removed.");
                        }
                    }
                }

                // Find appointment
                if (input.Contains(Constants.FindAppointment))
                {
                    string timeString = input.Split($"{Constants.FindAppointment} ")[1];

                    DateTime startTime = GetDateTime(timeString);

                    using (var context = new AppDbContext())
                    {
                        // creates db if not exists 
                        context.Database.EnsureCreated();

                        var appointment = context.Appointments.Where(a => a.AppointmentStartTime == startTime).FirstOrDefault();
                        if (appointment == null)
                        {
                            Console.WriteLine("No appointment was found.");
                        }
                        else
                        {
                            Console.WriteLine($"Found appointment starting at {startTime}.");
                        }
                    }
                }

                // Keep appointment
                if (input.Contains(Constants.KeepAppointment))
                {
                    string timeString = input.Split($"{Constants.KeepAppointment} ")[1];

                    DateTime startTime = GetRandomDateTime(timeString);

                    bool validStartTime = CheckValidDateTime(startTime);

                    if (validStartTime)
                    {
                        DateTime endTime = startTime.AddMinutes(Constants.AppointmentDuration);

                        using (var context = new AppDbContext())
                        {
                            // creates db if not exists 
                            context.Database.EnsureCreated();

                            var appointment = new Appointment() { AppointmentStartTime = startTime, AppointmentEndTime = endTime };
                            context.Appointments.Add(appointment);

                            context.SaveChanges();

                            Console.WriteLine($"Appointment starting at {startTime} has been booked.");
                        }
                    }

                }
            }
            else
            {
                Console.WriteLine("Invalid input provided. Input must contain the keyword ADD, DELETE, FIND or KEEP.");
            }
        }

        public static DateTime GetDateTime(string input)
        {
            DateTime result = new DateTime();
            result = DateTime.ParseExact(input, "dd/MM hh:mm", CultureInfo.InvariantCulture);

            return result;
        }

        public static bool CheckValidDateTime(DateTime startTime)
        {
            TimeSpan start = new TimeSpan(9, 0, 0);
            TimeSpan end = new TimeSpan(17,0,0);

            TimeSpan current = startTime.TimeOfDay;
            
            // Check if time is between 9am and 5pm
            if ((current < start) || (current > end))
            {
                Console.WriteLine($"Time entered is not between 9am and 5pm.");
                return false;
            }
            else
            {
                return true;
            }
        }

        public static DateTime GetRandomDateTime(string input)
        {
            // Get hours and minutes from input
            DateTime inputTime = DateTime.ParseExact(input, "hh:mm", CultureInfo.InvariantCulture);

            DateTime today = DateTime.Today;

            // Generate a random date in the future
            Random gen = new Random();
            int range = 365;
            DateTime randomDate = today.AddDays(gen.Next(range));

            // Add hours and minutes from input to the random date
            randomDate = randomDate.AddHours(inputTime.Hour);
            randomDate = randomDate.AddMinutes(inputTime.Minute);

            return randomDate;
        }
    }
}
