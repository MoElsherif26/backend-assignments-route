#region Fluent API Mapping in DbContext
using System.ComponentModel.DataAnnotations.Schema;

public class Course
{
    public int ID { get; set; }
    public int Duration { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    [ForeignKey(nameof(Topic))]
    public int Top_ID { get; set; }
    public virtual Topic Topic { get; set; }
}
#endregion