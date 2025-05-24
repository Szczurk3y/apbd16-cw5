using System.ComponentModel.DataAnnotations;

namespace apbd_cw5.Models;

public class Medicament
{
    [Key]
    public int MedicamentId { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }
    [MaxLength(100)]
    public string Description { get; set; }
    [MaxLength(100)]
    public string Type { get; set; }
    
    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

}