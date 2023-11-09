using Collaborative_Resource_Management_System.Models.Interfaces;

namespace Collaborative_Resource_Management_System.Models
{
    public class DepartmentService : IDepartmentService
    {
        private readonly AppDbContext _context;
        private readonly string _loggedInUserName;

        public DepartmentService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _loggedInUserName = configuration["DefaultUser"] ?? "Default User";
        }

        public async Task AddDepartmentAsync(Department department)
        {
            department.CreatedDate = DateTime.UtcNow;
            department.EditedDate = DateTime.UtcNow;
            department.CreatedBy = _loggedInUserName;
            department.EditedBy = _loggedInUserName;

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
        }
    }

}
