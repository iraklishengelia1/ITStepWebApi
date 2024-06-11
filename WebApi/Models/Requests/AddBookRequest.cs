using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Requests
{
    public class AddBookRequest
    {

        [Required]
        [MinLength(6)]
        public string Description { get; set; }

        [Required]
        [MinLength(2)]
        public string Title { get; set; }
        [Range(1d, 100d)]
        [Required]
        public double Price { get; set; }
        [Range(1,10)]
        [Required]
        public int Count { get; set; }
    }
}
