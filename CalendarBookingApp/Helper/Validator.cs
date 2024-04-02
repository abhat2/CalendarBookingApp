using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
        public static DateTime GetDateTime(string input)
        {
            DateTime result = new DateTime();
            result = DateTime.ParseExact(input, "dd/MM HH:mm", CultureInfo.InvariantCulture);

            return result;
        }

        public static bool CheckValidDateTime(DateTime startTime)
        {
            TimeSpan start = new TimeSpan(9, 0, 0);
            TimeSpan end = new TimeSpan(17,0,0);

            TimeSpan current = startTime.TimeOfDay;
            
            // Check if time is not between 9am and 5pm
            if ((current < start) || (current > end))
            {
                Console.WriteLine($"Time entered is not between 9am and 5pm.");
                return false;
            }
            else
            {
                // Check if it is 2nd day of 3rd week
                int weekNumber = GetWeekNumberOfMonth(startTime);
                int dayNumber = ((int)startTime.DayOfWeek == 0) ? 7 : (int)startTime.DayOfWeek;

                if (weekNumber == 3) {
                   if (dayNumber == 2)
                    {
                        TimeSpan start4pm = new TimeSpan(16, 0, 0);
                        TimeSpan end5pm = new TimeSpan(17, 0, 0);

                        if ((current >= start4pm) && (current <= end5pm))
                        {
                            Console.WriteLine("Please enter another date and time, the one you have entered has been reserved.");
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    } 
                }

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

        public static int GetWeekNumberOfMonth(DateTime date)
        {
            date = date.Date;
            DateTime firstMonthDay = new DateTime(date.Year, date.Month, 1);
            DateTime firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            if (firstMonthMonday > date)
            {
                firstMonthDay = firstMonthDay.AddMonths(-1);
                firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            }
            return (date - firstMonthMonday).Days / 7 + 1;
        }
    }
}
