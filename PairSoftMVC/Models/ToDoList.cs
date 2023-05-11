using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PairSoftMVC.Models
{
    public class ToDoList
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Task title must be Enter")]
        [StringLength(30, ErrorMessage = "Title Lenght Must be Less Than 30")]
        public string? Title { get; set; }
        [StringLength(100, ErrorMessage = "Title Lenght Must be Less Than 100")]
        public string? Description { get; set; }
        [Required(ErrorMessage ="DueDute Is Required")]
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
