using Collaborative_Resource_Management_System.Data;
using Collaborative_Resource_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Collaborative_Resource_Management_System.Controllers
{
    public class ReportController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ReportController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Report()
        {
            var checkouts = _dbContext.CheckOuts
                            .Include(co => co.User)
                            .Include(co => co.Item)
                            .Include(co => co.Department)
                            .Select(co => new CheckOutViewModel
                            {
                                User = co.User.Name,
                                Item = co.Item.Name,
                                CheckOutDate = co.CheckOutDate,
                                Price = co.TotalPrice,
                                Department = co.Department.Name
                            })
                            .ToList();
            return View(checkouts);
        }

        public IActionResult Confirmation()
        {
            return View();
        }
    }
}
