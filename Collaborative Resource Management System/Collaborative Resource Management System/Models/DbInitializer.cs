using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Collaborative_Resource_Management_System.Models
{
    public static class DbInitializer
    {
        public static void SeedData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServiceProvider = scope.ServiceProvider;
                var context = scopedServiceProvider.GetRequiredService<AppDbContext>();

               
            }
        }
    }
}
