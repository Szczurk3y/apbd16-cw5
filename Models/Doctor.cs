using System.ComponentModel.DataAnnotations;

namespace apbd_cw5.Models;

public class Doctor
{
    [Key]
    public int DoctorId { get; set; }
    [MaxLength(100)]
    public string FirstName { get; set; }
    [MaxLength(100)]
    public string LastName { get; set; }
    [MaxLength(100)]
    public string Email { get; set; }
    
    public ICollection<Prescription> Prescriptions { get; set; }
}