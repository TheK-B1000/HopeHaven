using System.ComponentModel.DataAnnotations;

namespace Collaborative_Resource_Management_System.Models
{
    public class Report
    {
        public int ReportID { get; set; }

        public string ReportName { get; set; }

        public string ReportDescription { get; set; }

        public DateTime ReportDate { get; set; }

        public string UserID { get; set; }

    }
}
