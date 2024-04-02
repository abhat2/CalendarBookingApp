using CalendarBookingApp.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarBookingApp.Services
{
    public interface IAppointmentService
    {
        Task AddAppointment(Appointment appointment);
    }
}
