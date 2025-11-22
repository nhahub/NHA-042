using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Online_Medical.Models

{
    public enum PaymentMethod
    {
        Cash,
        Visa,
        PayPal,
        Insurance 
    }
    public enum PaymentStatus
    {
        Pending,  
        Completed,
        Failed     
    }
    public class Payment
    {
        public int Id { get; set; } 

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; } = DateTime.Now; 

 
        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
        // Foreign Key
        public int AppointmentId { get; set; }
        public Appointment Appointment { get; set; }
    }
}
