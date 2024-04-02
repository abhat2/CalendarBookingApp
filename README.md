# Calendar Booking App
This is a calendar booking console application built in .NET version 8. SQL Server Express LocalDB is used for the database.

## Instructions
1. Clone repository.
2. Preffered IDE is to use Visual Studio.
3. Open the solution file.
4. On your local SQL Server, create a database with the name **CalendarBooking**.
5. Create a table on the database with the name Appointments and the following fields:
   1. AppointmentId - bigint (Primary Key)
   1. AppointmentStartTime - datetime
	1. AppointmentEndTime - datetime
6. Run the console application in Visual Studio.

## Features
The application accepts the following commands from the command line:
- ADD DD/MM hh:mm to add an appointment.
- DELETE DD/MM hh:mm to remove an appointment.
- FIND DD/MM to find a free timeslot for the day.
- KEEP hh:mm keep a timeslot for any day.
- The time slot is equal to 30 minutes.

The application can assign any slot on any day, except for the following constraints:
- The acceptable time is between 9AM and 5PM
- Except from 4 PM to 5 PM on each second day of the third week of any month - this must be reserved and unavailable

## Improvements
I would make the following improvements to the application if I had spent 2 days:
- Adding logging to the application and using the different categories (i.e. INFO, ERROR) - this will make it easier for user to identify errors.
- Adding unit tests to verify the validation functions.
- Simplifying if statements which handle different commands in the Program class.
- Error handling for specific scenarios.