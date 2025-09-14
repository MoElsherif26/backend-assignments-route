#region Fluent API Mapping in DbContext
using System.ComponentModel.DataAnnotations.Schema;

public class CourseInst
{
    [ForeignKey(nameof(Instructor))]
    public int Inst_ID { get; set; }
    [ForeignKey(nameof(Course))]
    public int Course_ID { get; set; }
    public string Evaluate { get; set; }
    public virtual Instructor Instructor { get; set; }
    public virtual Course Course { get; set; }
}
#endregion