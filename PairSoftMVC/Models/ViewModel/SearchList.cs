using System.ComponentModel.DataAnnotations;

namespace PairSoftMVC.Models.ViewModel
{
    public class SearchList
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
