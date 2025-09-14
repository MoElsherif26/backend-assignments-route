#region Convention Mapping
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore01.Entities
{

    public class Student
    {
        public int ID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        [ForeignKey(nameof(Department))]
        public int Dep_Id { get; set; }
        public virtual Department Department { get; set; }
    }
}
#endregion