#region Data Annotations Mapping
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Instructor
{
    [Key]
    public int ID { get; set; }

    [Required, StringLength(100)]
    public string Name { get; set; }

    public decimal Bonus { get; set; }
    public decimal Salary { get; set; }

    [StringLength(200)]
    public string Address { get; set; }

    public decimal HourRate { get; set; }
    [ForeignKey(nameof(Department))]
    public int Dept_ID { get; set; }
    public virtual Department Department { get; set; }
}
#endregion