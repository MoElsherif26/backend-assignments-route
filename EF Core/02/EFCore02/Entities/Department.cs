#region Fluent API Mapping in DbContext
using System.ComponentModel.DataAnnotations.Schema;

public class Department
{
    public int ID { get; set; }
    public string Name { get; set; }
    [ForeignKey(nameof(Instructor))]
    public int Ins_ID { get; set; }
    public DateTime HiringDate { get; set; }
    //public virtual Instructor Instructor { get; set; }
}
#endregion