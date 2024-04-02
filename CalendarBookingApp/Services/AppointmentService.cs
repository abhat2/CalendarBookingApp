using CalendarBookingApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarBookingApp.Services
{
    public class AppointmentService: IAppointmentService
    {
        private readonly AppDbContext _dbContext;

        public AppointmentService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Insert appointment into database
        public void AddAppointment(Appointment appointment)
        {
            _dbContext.Appointments.Add(appointment);
            _dbContext.SaveChanges();
            Console.WriteLine($"Appointment starting at {appointment.AppointmentStartTime} has been booked.");
        }

        // Delete appointment from database based on start time
        public void DeleteAppointment(DateTime startTime)
        {
            Appointment? appointment = _dbContext.Appointments.Where(a => a.AppointmentStartTime == startTime).FirstOrDefault();
            if (appointment == null)
            {
                Console.WriteLine("No appointment was found.");
            }
            else
            {
                _dbContext.Remove(appointment);
                _dbContext.SaveChanges();

                Console.WriteLine($"Appointment starting at {startTime} has been removed.");
            }
        }

        // Get appointment from database based on start time
        public void FindAppointment(DateTime startTime)
        {
            Appointment? appointment = _dbContext.Appointments.Where(a => a.AppointmentStartTime == startTime).FirstOrDefault();
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
}
