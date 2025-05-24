using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apbd_cw5.Models;

public class PrescriptionMedicament
{
    [ForeignKey(nameof(Medicament))]
    public int MedicamentId { get; set; }
    [ForeignKey(nameof(Prescription))]
    public int PrescriptionId { get; set; }
    
    public Medicament Medicament { get; set; }
    public Prescription Prescription { get; set; }
    
    public int Dose { get; set; }
    
    [MaxLength(100)]
    public string Details { get; set; }
}