using System.ComponentModel.DataAnnotations;

namespace Collaborative_Resource_Management_System.Models
{
    public class User
    {
        public int UserID { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public int PIN { get; set; }
    }
}
