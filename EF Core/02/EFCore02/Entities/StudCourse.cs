#region Data Annotations with EF Core
using EFCore01.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

[PrimaryKey(nameof(Stud_ID), nameof(Course_ID))]
[Table("Stud_Course")]
public class StudCourse
{
    [ForeignKey(nameof(Student))]
    public int Stud_ID { get; set; }
    [ForeignKey(nameof(Course))]
    public int Course_ID { get; set; }
    public string Grade { get; set; }
    public virtual Student Student { get; set; }
    public virtual Course Course { get; set; }
}
#endregion