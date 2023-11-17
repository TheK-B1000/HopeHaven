namespace Collaborative_Resource_Management_System.Models
{
    public class CheckOutViewModel
    {
        public string User { get; set; }
        public string Item { get; set; }
        public DateTime CheckOutDate { get; set; }
        public float Price { get; set; }
        public string Department { get; set; }
    }
}
