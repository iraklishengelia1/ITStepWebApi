using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Requests
{
    public class BuyBookRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
