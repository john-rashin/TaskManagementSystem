using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Models
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public bool IsComplete { get; set; }
    }
}
