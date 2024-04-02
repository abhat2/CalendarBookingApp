using CalendarBookingApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarBookingApp.Services
{
    public interface IAppointmentService
    {
        void AddAppointment(Appointment appointment);
        void DeleteAppointment(DateTime startTime);
        void FindAppointment(DateTime startTime);
    }
}
