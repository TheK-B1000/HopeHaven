using System.ComponentModel.DataAnnotations;

namespace Collaborative_Resource_Management_System.Models
{
    public class PinLoginViewModel
    {
        [Required]
        [StringLength(6, ErrorMessage = "The PIN must be 6 digits long", MinimumLength = 6)]
        [Display(Name = "PIN")]
        public string Pin { get; set; }
        public string Username { get; internal set; }
    }

}
