using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PairSoftAPI.Models
{
    public class ToDoList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string? Title { get; set; }
        [StringLength(100)]
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        [Required]
        public bool IsCompleted { get; set; }
    }
}
