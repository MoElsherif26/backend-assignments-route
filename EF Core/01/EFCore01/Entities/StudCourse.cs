#region Data Annotations with EF Core
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

[PrimaryKey(nameof(Stud_ID), nameof(Course_ID))]
[Table("Stud_Course")]
public class StudCourse
{
    public int Stud_ID { get; set; }
    public int Course_ID { get; set; }
    public string Grade { get; set; }
}
#endregion