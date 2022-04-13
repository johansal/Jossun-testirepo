using System.ComponentModel.DataAnnotations;

namespace BookStoreApp.API.Models
{
    public class BaseDto
    {
        [Required]
        public int Id { get; set; }
    }
}
