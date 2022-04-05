namespace HealthCare.Contracts.Models.Appointment.Requests
{
    using System;
    using Utilities.Enums.Appointment;

    public class RequestAppointmentRequest
    {
        public long IdentityNumber {get;set;}

        public int MedicalManInfoId { get; set; }
      
        public int AppointmentHourId { get; set; }

        public DateTime AppointmentDate { get; set; }

        public AppointmentReason AppointmentReason { get; set; }
    }
}
