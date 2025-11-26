using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Medical.Models
{
    public enum AppointmentStatus
    {
        Pending,    
        Confirmed, 
        Completed,  
        Cancelled  
    }
    public enum PaymentStatusforAppointment
    {
        Unpaid,    
        Paid        
    }
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
       
       public PaymentStatusforAppointment PaymentStatus { get; set; } = PaymentStatusforAppointment.Unpaid;
        public Payment? Payment { get; set; }
        // Foreign Keys
        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        [ForeignKey("Patient")]
        public string PatientId { get; set; }
        public Patient Patient { get; set; }
        [ForeignKey("Clinic")]
        public int ClinicId { get; set; }
        public Clinic Clinic { get; set; }









    }
}
