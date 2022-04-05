namespace HealthCare.Contracts.Models.Appointment.Responses
{
    using System.Collections.Generic;
    using Common;
    using Data;

    public class GetAppointmentsResponse : TokenData
    {
        public List<AppointmentInfo> Appointments {get;set;}
    }
}
