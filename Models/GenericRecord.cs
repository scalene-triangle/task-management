using System.ComponentModel.DataAnnotations;

namespace task_management.Models
{
    public abstract class GenericRecord
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
