using System.ComponentModel.DataAnnotations;

namespace PairSoftAPI.Models
{
    public class SearchList
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
