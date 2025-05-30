using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apbd_cw5.Models;

public class Prescription
{
    [Key]
    public int PrescriptionId { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    
    [ForeignKey(nameof(Patient))]
    public int PatientId { get; set; }
    [ForeignKey(nameof(Doctor))]
    public int DoctorId { get; set; }
    
    public Patient Patient { get; set; }
    public Doctor Doctor { get; set; }      
    
    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    public override string ToString()
    {
        return $"""
                PrescriptionId: {PrescriptionId}
                Date: {Date}
                DueDate: {DueDate}
                Patient: {Patient}
                Doctor: {Doctor}
                """;
    }
}