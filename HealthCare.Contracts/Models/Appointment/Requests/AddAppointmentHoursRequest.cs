namespace HealthCare.Contracts.Models.Appointment.Requests
{
    using System.Collections.Generic;

    public class AddAppointmentHoursRequest
    {
        public int MedicalCenterId { get;set; }

        public List<HourData>  Hours { get; set; }
    }
}
