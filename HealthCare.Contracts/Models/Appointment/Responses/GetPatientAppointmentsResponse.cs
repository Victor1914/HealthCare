namespace HealthCare.Contracts.Models.Appointment.Responses
{
    using System.Collections.Generic;
    using Common;
    using Data;

    public class GetPatientAppointmentsResponse : TokenData
    {
        public List<PatientAppointment> Appointments { get; set; }
    }

    public class PatientAppointment : AppointmentBaseInfo
    {
        public string DoctorName { get; set; }

        public string Specialty { get; set; }

        public bool IsCurrentDoctor { get; set; }
    }
}